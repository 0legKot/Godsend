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
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Upload()
        {
            #region constants
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
            const double maxImageProportionCoef = 10.0;
            const double minImageProportionCoef = 1.0 / maxImageProportionCoef;
            #endregion
            var files = Request.Form.Files;
            List<SKBitmap> skImages = new List<SKBitmap>();
            if (files.Count > maxImagesPerUpload)
            {
                return BadRequest("Too Many Images");
            }

            foreach (var file in files)
            {
                if (file.Length != 0)
                {
                    try
                    {
                        CheckExtensionAndFileLength(allowedImageExtensions, maxImageFileLength, file);
                        SKBitmap image = GetImageFromFile(file);
                        CheckSize(maxImageWidth, maxImageHeight, minImageWidth, minImageHeight, maxImageProportionCoef, minImageProportionCoef, image);
                        skImages.Add(image);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
            }

            var images = new List<Image>();
            ProcessImages(resizeImageWidth, resizeImageHeight, thumbWidth, thumbHeight, hashThumbWidth, hashThumbHeight, skImages, ref images);
            return Ok(images);
        }

        private static SKBitmap GetImageFromFile(IFormFile file)
        {
            SKBitmap image;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Position = 0;
                image = SKBitmap.Decode(ms);
            }

            if (image == null)
            {
                throw new Exception("Broken image");
            }

            return image;
        }

        private static void CheckSize(int maxImageWidth, int maxImageHeight, int minImageWidth, int minImageHeight, double maxImageProportionCoef, double minImageProportionCoef, SKBitmap image)
        {
            if (image.Width < minImageWidth || image.Height < minImageHeight)
            {
                throw new Exception("Too Small Image");
            }

            if (image.Width > maxImageWidth || image.Height > maxImageHeight)
            {
                throw new Exception("Too Big Image");
            }

            double proportion = (double)image.Width / image.Height;
            if (proportion > maxImageProportionCoef || proportion < minImageProportionCoef)
            {
                throw new Exception("Invalid Proportions");
            }
        }

        private static void CheckExtensionAndFileLength(List<string> allowedImageExtensions, int maxImageFileLength, IFormFile file)
        {
            if (file.Length > maxImageFileLength)
            {
                throw new Exception("File is to big");
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedImageExtensions.Contains(extension))
            {
                throw new Exception($"Extension {extension} not allowed");
            }
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
                    }
                    else
                    {
                        var newId = Guid.NewGuid();
                        Image newImage = new Image
                        {
                            Id = newId,
                            Thumb = hashThumbnail,
                            Path = newId.ToString() + ".jpg",
                        };

                        // Saving FULL to storage
                        using (MemoryStream fullMs = _imageService.ConvertToStream(image))
                        {
                            _storageService.SaveToStorage(newImage.Path, fullMs);
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
                }

                img.Dispose();
                skImages[i].Dispose();
            }
        }
    }
}