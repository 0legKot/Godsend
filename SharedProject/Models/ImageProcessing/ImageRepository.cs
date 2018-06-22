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
        public ImageRepository(DataContext ctx)
        {
            context = ctx;
            if (!context.ImagePathsTable.Any(ipt => ipt.Id == context.Products.Include(s => s.Info).FirstOrDefault().Info.Id))
            {
                foreach (Product sp in ctx.Products.Include(x => x.Info))
                {
                    context.ImagePathsTable.Add(new ImagePaths() { Id = sp.Info.Id, Preview = "apple.jpg", Images = new List<StringWrapper>() { "apple.jpg", "pineapple.jpg" } });
                }

                context.SaveChanges();
            }

            // ensure it's seeded
            var supRepo = new EFSupplierRepository(ctx);

            if (!context.ImagePathsTable.Any(ipt => ipt.Id == context.Suppliers.Include(s => s.Info).FirstOrDefault().Info.Id))
            {
                foreach (Supplier ss in ctx.Suppliers.Include(x => x.Info))
                {
                    context.ImagePathsTable.Add(new ImagePaths() { Id = ss.Info.Id, Preview = "suppApple.jpg" });
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<string> GetImages(Guid id)
        {
            return context.ImagePathsTable.Include(x => x.Images).FirstOrDefault(x => x.Id == id).Images.Select(x => x.Value);
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
