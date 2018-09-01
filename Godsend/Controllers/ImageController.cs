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
        /// Gets the preview image.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Base64-encoded image</returns>
        [HttpGet("[action]/{id:Guid}")]
        public JsonResult GetPreviewImage(Guid id)
        {
            try
            {
                return Json(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + repository.GetImage(id))));
            }
            catch
            {
                // no image for id (new entities)
                return null;
            }

            ////return File(new FileStream("Images/"+repository.GetImage(id), FileMode.Open, FileAccess.Read), "image/jpeg");
        }

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Array of base64-encoded images</returns>
        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<string> GetImages(Guid id)
        {
            var images = new List<string>();
            foreach (string fileName in repository.GetImages(id))
            {
                images.Add(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + fileName)));
            }

            return images;
        }

        /// <summary>
        /// Gets the preview images.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>dictionary of (id, base64-encoded image) pairs</returns>
        [HttpPost("[action]")]
        public IDictionary<Guid, string> GetPreviewImages([FromBody]Guid[] ids)
        {
            var res = new Dictionary<Guid, string>();

            foreach (Guid id in ids)
            {
                var tmp = GetPreviewImage(id);
                if (tmp != null)
                {
                    res.Add(id, GetPreviewImage(id).Value.ToString());
                }
            }

            return res;
        }

        // NUTRIX

        private readonly IImageService _imageService;
        private readonly IStorageService _storageService;
        private readonly ICryptoService _cryptoService;
        private readonly ImageRepository repository;

        public ImageController(ImageRepository repo, IImageService imageService, IStorageService storageService, ICryptoService cryptoService)
        {
            _imageService = imageService;
            _storageService = storageService;
            _cryptoService = cryptoService;
            this.repository = repo;
        }

        [HttpPost("upload/{id:Guid}")]
        public IActionResult Upload(Guid id)
        {
            var files = Request.Form.Files;

            const string allowedImageExtensions = ".jpg .png .jpeg .bmp";
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

            List<SKBitmap> images = new List<SKBitmap>();

            // Validation
            if (files.Count > maxImagesPerUpload)
                throw new BadRequestException("errorTooManyImages");
            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                // File size
                if (file.Length > maxImageFileLength)
                    throw new BadRequestException("errorNoFileSize");

                // Extension
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedImageExtensions.Contains(extension))
                    throw new BadRequestException("errorOnlyImagesAllowed");

                // Get image, Validate it's actually image
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
                    throw new BadRequestException("errorBrokenImage");
                }

                // Image size and proportions
                int width = image.Width;
                int height = image.Height;
                if (width < minImageWidth || height < minImageHeight)
                    throw new BadRequestException("errorTooSmallImage");
                if (width > maxImageWidth || height > maxImageHeight)
                    throw new BadRequestException("errorTooBigImage");
                double proportion = width / height;
                if (proportion < 1 / maxImageProportionCoef || proportion > 1 * maxImageProportionCoef)
                    throw new BadRequestException("errorInvalidProportions");

                images.Add(image);
            }

            // Processing
            var fileNames = new List<string>();
            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];

                // Resizing image if needed
                if (image.Width > resizeImageWidth || image.Height > resizeImageHeight)
                    image = _imageService.ResizeImage(image, resizeImageWidth, resizeImageHeight);
                else
                    image = _imageService.ResizeImage(image, image.Width, image.Height); // To prevent GDI+ Exception

                // Getting hash thumbnail (to prevent hash colisions)
                string hashThumbnail = _imageService.GetThumbnail(image, hashThumbWidth, hashThumbHeight);

                // Converting to stream
                MemoryStream fullMs = _imageService.ConvertToStream(image);

                // Check for duplicate
                Image original = repository.GetImageByThumb(hashThumbnail);

                // Return original, dont save duplicate
                if (original != null)
                {
                    fileNames.Add(original.Path);

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
                _storageService.SaveToStorage(fileName, fullMs);
                fileNames.Add(fileName);
                repository.AddImage(newImage, id);

                // Converting thumb
                var thumbImage = _imageService.ResizeImage(image, thumbWidth, thumbHeight);
                var thumbMs = _imageService.ConvertToStream(thumbImage);

                // Saving THUMB to storage
                _storageService.SaveToStorage(newImage.Id.ToString() + "s.jpg", thumbMs);

                fullMs?.Dispose();
                thumbMs?.Dispose();
                image?.Dispose();
            }

            // Result
            return Ok(fileNames);
        }
    }

    public class CryptoService : ICryptoService
    {
        public long GetHash(Stream stream)
        {
            var md5 = GetMD5CheckSum(stream);
            var hash = GetInt64HashCode(md5);
            return hash;
        }

        public long GetHash(string str)
        {
            var hash = GetInt64HashCode(str);
            return hash;
        }

        // Modified version of
        // https://stackoverflow.com/questions/10520048/calculate-md5-checksum-for-a-file
        private string GetMD5CheckSum(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        // Modified version of
        // https://www.codeproject.com/Articles/34309/Convert-String-to-64bit-Integer
        private long GetInt64HashCode(string strText)
        {
            long hashCode = 0;
            if (!string.IsNullOrEmpty(strText))
            {
                byte[] byteContents = Encoding.Unicode.GetBytes(strText);
                using (SHA256 hash = new SHA256CryptoServiceProvider())
                {
                    byte[] hashText = hash.ComputeHash(byteContents);
                    long hashCodeStart = BitConverter.ToInt64(hashText, 0);
                    long hashCodeMedium = BitConverter.ToInt64(hashText, 8);
                    long hashCodeEnd = BitConverter.ToInt64(hashText, 24);
                    hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
                }
            }
            return (hashCode);
        }
    }

    public class ImageService : IImageService
    {
        const int quality = 75;
        const SKBitmapResizeMethod resizeMethod = SKBitmapResizeMethod.Lanczos3;

        public string GetThumbnail(SKBitmap image, int width, int height)
        {
            using (var scaledImage = ResizeImage(image, width, height))
            {
                SKBitmap.Resize(scaledImage, image, resizeMethod);

                using (var memoryStream = new SKDynamicMemoryWStream())
                {
                    scaledImage.Encode(memoryStream, SKEncodedImageFormat.Jpeg, quality);
                    return Convert.ToBase64String(memoryStream.DetachAsData().ToArray());

                }
            }
        }

        public SKBitmap ResizeImage(SKBitmap image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new SKBitmap(newWidth, newHeight);
            SKBitmap.Resize(newImage, image, resizeMethod);

            return newImage;
        }

        public MemoryStream ConvertToStream(SKBitmap image)
        {
            var memstream = new MemoryStream();

            using (var ms = new SKManagedWStream(memstream))
            {
                image.Encode(ms, SKEncodedImageFormat.Jpeg, quality);
            }

            memstream.Seek(0, SeekOrigin.Begin);
            return memstream;
        }
    }

    public class StorageService : IStorageService
    {
        const string storagePath = "Images/";

        public FileStream GetFromStorage(string outerFileName)
        {
            var pathToFile = storagePath + outerFileName;

            if (!File.Exists(pathToFile))
                throw new NotFoundException("errorFileNotFound");

            return File.OpenRead(pathToFile);
        }

        public void SaveToStorage(string fileName, Stream contents)
        {
            string path = storagePath + fileName;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                contents.Seek(0, SeekOrigin.Begin);
                contents.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }

    public interface ICryptoService
    {
        long GetHash(Stream stream);

        long GetHash(string str);
    }

    public interface IImageService
    {
        string GetThumbnail(SKBitmap image, int width, int height);

        SKBitmap ResizeImage(SKBitmap image, int maxWidth, int maxHeight);

        MemoryStream ConvertToStream(SKBitmap image);
    }

    public interface IStorageService
    {
        FileStream GetFromStorage(string outerFileName);

        void SaveToStorage(string fileName, Stream contents);
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}