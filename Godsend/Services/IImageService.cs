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

    public interface IImageService
    {
        string GetThumbnail(SKBitmap image, int width, int height);

        SKBitmap ResizeImage(SKBitmap image, int maxWidth, int maxHeight);

        MemoryStream ConvertToStream(SKBitmap image);
    }
}