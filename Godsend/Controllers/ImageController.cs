// <copyright file="ImageController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SkiaSharp;

    /// <summary>
    /// Image controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>image as file</returns>
        [HttpGet("[action]/{id:Guid}")]
        public ActionResult GetImage(Guid id)
        {
            try
            {
                return File(new FileStream("Images/" + repository.GetImage(id), FileMode.Open, FileAccess.Read), "image/jpeg");
                //return Json(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + repository.GetImage(id))));
            }
            catch
            {
                // no image for id (new entities)
                return BadRequest();
            }
        }

        #region  deprecated
        //[HttpPost("[action]")]
        //public IEnumerable<string> GetImages([FromBody]Guid[] ids)
        //{
        //    var images = new List<string>();
        //    foreach (Guid id in ids)
        //    {
        //        images.Add(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + repository.GetImage(id))));
        //    }

        //    return images;
        //}

        //[HttpPost("[action]")]
        //public IDictionary<Guid, string> GetPreviewImages([FromBody]Guid[] ids)
        //{
        //    var res = new Dictionary<Guid, string>();

        //    foreach (Guid id in ids)
        //    {
        //        var tmp = GetPreviewImage(id);
        //        if (tmp != null)
        //        {
        //            res.Add(id, GetPreviewImage(id).Value.ToString());
        //        }
        //    }

        //    return res;
        //}
        #endregion

        // NUTRIX

        private readonly IImageService _imageService;
        private readonly IStorageService _storageService;
        private readonly ImageRepository repository;

        public ImageController(ImageRepository repo, IImageService imageService, IStorageService storageService)
        {
            _imageService = imageService;
            _storageService = storageService;
            this.repository = repo;
        }

        [HttpPost("upload")]
        public IActionResult Upload()
        {
            var files = Request.Form.Files;

            List<string> allowedImageExtensions = new List<string> { ".jpg", ".png", ".jpeg", ".bmp" };
            const int maxImagesPerUpload = 5;
            const int resizeImageWidth = 2000;
            const int resizeImageHeight = 2000;
            const int maxImageFileLength = 20971520; // 20 megabytes
            const int maxImageWidth = 15000;
            const int maxImageHeight = 15000;
            const int minImageWidth = 128;
            const int minImageHeight = 128;
            const int thumbWidth = 360;
            const int thumbHeight = 360;
            const int hashThumbWidth = 10;
            const int hashThumbHeight = 10;
            const int maxImageProportionCoef = 4;

            List<SKBitmap> skImages = new List<SKBitmap>();

            // Validation
            if (files.Count > maxImagesPerUpload)
            {
                return BadRequest("Too Many Images");
            }

            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    continue;
                }

                if (file.Length > maxImageFileLength)
                {
                    return BadRequest("No File Size");
                }

                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedImageExtensions.Contains(extension))
                {
                    return BadRequest($"Extension {extension} not allowed");
                }

                SKBitmap image = null;
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        ms.Position = 0;
                        image = SKBitmap.Decode(ms);
                    }
                }
                catch
                {
                    return BadRequest("Broken Image");
                }

                int width = image.Width;
                int height = image.Height;
                if (width < minImageWidth || height < minImageHeight)
                {
                    return BadRequest("Too Small Image");
                }

                if (width > maxImageWidth || height > maxImageHeight)
                {
                    return BadRequest("Too Big Image");
                }

                double proportion = width / height;
                if (proportion < 1 / maxImageProportionCoef || proportion > 1 * maxImageProportionCoef)
                {
                    return BadRequest("Invalid Proportions");
                }

                skImages.Add(image);
            }

            // Processing
            var images = new List<Image>();
            ProcessImages(resizeImageWidth, resizeImageHeight, thumbWidth, thumbHeight, hashThumbWidth, hashThumbHeight, skImages, ref images);

            // Result
            return Ok(images);
        }

        private void ProcessImages(int resizeImageWidth, int resizeImageHeight, int thumbWidth, int thumbHeight, int hashThumbWidth, int hashThumbHeight, List<SKBitmap> skImages, ref List<Image> images)
        {
            for (int i = 0; i < skImages.Count; i++)
            {
                SKBitmap img;
                if (skImages[i].Width > resizeImageWidth || skImages[i].Height > resizeImageHeight)
                {
                    img = _imageService.ResizeImage(skImages[i], resizeImageWidth, resizeImageHeight);
                }
                else
                {
                    img = _imageService.ResizeImage(skImages[i], skImages[i].Width, skImages[i].Height); // To prevent GDI+ Exception
                }

                using (var image = img)
                {

                    // to prevent hash colisions
                    string hashThumbnail = _imageService.GetThumbnail(image, hashThumbWidth, hashThumbHeight);

                    // Check for duplicate
                    Image original = repository.GetImageByThumb(hashThumbnail);

                    // Return original, dont save duplicate
                    if (original != null)
                    {
                        images.Add(original);

                        continue;
                    }

                    // Add entry to db and get ID
                    Image newImage = new Image
                    {
                        Id = Guid.NewGuid(),
                        Thumb = hashThumbnail,
                    };

                    string fileName = newImage.Id.ToString() + ".jpg";
                    newImage.Path = fileName;

                    // Saving FULL to storage
                    using (MemoryStream fullMs = _imageService.ConvertToStream(image))
                    {
                        _storageService.SaveToStorage(fileName, fullMs);
                    }

                    images.Add(newImage);
                    repository.AddImage(newImage);

                    // Converting thumb
                    var thumbImage = _imageService.ResizeImage(image, thumbWidth, thumbHeight);
                    using (MemoryStream thumbMs = _imageService.ConvertToStream(thumbImage))
                    {
                        _storageService.SaveToStorage(newImage.Id.ToString() + "s.jpg", thumbMs);
                    }
                }

                img.Dispose();
            }
        }
    }
}