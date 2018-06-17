// <copyright file="ImagePaths.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Godsend.Models;

    public class ImagePaths
    {
        public Guid Id { get; set; }

        public string Preview { get; set; }

        public IEnumerable<StringWrapper> Images { get; set; }
    }
}
