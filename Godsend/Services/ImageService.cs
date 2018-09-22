// <copyright file="ImageController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend
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
}