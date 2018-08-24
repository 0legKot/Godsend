// <copyright file="ImageRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///
    /// </summary>
    public class ImageRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public ImageRepository(DataContext ctx, ISeedHelper seedHelper)
        {
            context = ctx;

            seedHelper.EnsurePopulated(ctx);
        }

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<string> GetImages(Guid id)
        {
            return context.ImagePathsTable.FirstOrDefault(x => x.Id == id)?.Images.Select(x => x.Value) ?? Enumerable.Empty<string>();
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string GetImage(Guid id)
        {
            return context.ImagePathsTable.FirstOrDefault(x => x.Id == id).Preview;
        }
    }
}
