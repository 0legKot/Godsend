// <copyright file="ImagePaths.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Godsend.Models;

    /// <summary>
    /// 
    /// </summary>
    public class ImagePaths
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the preview.
        /// </summary>
        /// <value>
        /// The preview.
        /// </value>
        public string Preview { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public IEnumerable<StringWrapper> Images { get; set; }
    }
}
