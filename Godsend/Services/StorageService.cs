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

    public class StorageService : IStorageService
    {
        const string storagePath = "Images/";

        public FileStream GetFromStorage(string outerFileName)
        {
            var pathToFile = storagePath + outerFileName;

            if (!File.Exists(pathToFile))
            {
                throw new NotFoundException($"File {outerFileName} Not Found");
            }

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
}