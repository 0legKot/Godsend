// <copyright file="DataContext.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// 
    /// </summary>
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DataContext(DbContextOptions<DataContext> options)
               : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the suppliers.
        /// </summary>
        /// <value>
        /// The suppliers.
        /// </value>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the articles.
        /// </summary>
        /// <value>
        /// The articles.
        /// </value>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public DbSet<Cell> Values { get; set; }

        /// <summary>
        /// Gets or sets the image paths table.
        /// </summary>
        /// <value>
        /// The image paths table.
        /// </value>
        public DbSet<ImagePaths> ImagePathsTable { get; set; }

        /// <summary>
        /// Gets or sets the link products suppliers.
        /// </summary>
        /// <value>
        /// The link products suppliers.
        /// </value>
        public DbSet<LinkProductsSuppliers> LinkProductsSuppliers { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EAV> LinkProductProperty { get; set; }
        ////public DbSet<ProductInformation> ProductInformation { get; set; }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<EAV>()
            //.HasKey(c => new { c.Product, c.Property });
            //builder.Entity<ProductWithSuppliers>()
            //.HasKey(c => new { c.Product, c.Suppliers });
            builder.Entity<SimpleOrder>();
            builder.Entity<SimpleProduct>();
            builder.Entity<ProductFruit>();
            builder.Entity<ProductTV>();
            ////builder.Entity<DiscreteProduct>();
            ////builder.Entity<WeightedProduct>();
            builder.Entity<SimpleArticle>();
            builder.Entity<SimpleSupplier>();
            ////builder.Entity<ProductInformation>();
            base.OnModelCreating(builder);
        }
    }
}
