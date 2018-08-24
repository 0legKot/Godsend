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
    using Microsoft.EntityFrameworkCore.Metadata;

    /// <summary>
    /// Data context that contains all tables. No separation because it is easier for hosting to have only one database
    /// </summary>
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class. Uses base call for everything
        /// </summary>
        /// <param name="options">The options.</param>
        public DataContext(DbContextOptions<DataContext> options)
               : base(options)
        {
        }

        /// <summary>
        /// Gets or sets table with products
        /// </summary>
        /// <value>
        /// Products and their cards, categories and properties (dictionary)
        /// </value>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets table with suppliers
        /// </summary>
        /// <value>
        /// Suppliers and their cards
        /// </value>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Gets or sets table of orders
        /// </summary>
        /// <value>
        /// Each order has list of products and general info, no information card for this
        /// </value>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets table with articles
        /// </summary>
        /// <value>
        /// Articles and their cards.
        /// </value>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Gets or sets table for string path for every image that must be sent to client
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

        public DbSet<EAV<int>> LinkProductPropertyInt { get; set; }

        public DbSet<EAV<string>> LinkProductPropertyString { get; set; }

        public DbSet<EAV<decimal>> LinkProductPropertyDecimal { get; set; }

        public DbSet<LinkRatingProduct> LinkRatingProduct { get; set; }

        public DbSet<LinkRatingSupplier> LinkRatingSupplier { get; set; }

        public DbSet<LinkRatingArticle> LinkRatingArticle { get; set; }

        public DbSet<LinkCommentProduct> LinkCommentProduct { get; set; }

        public DbSet<LinkCommentSupplier> LinkCommentSupplier { get; set; }

        public DbSet<LinkCommentArticle> LinkCommentArticle { get; set; }

        /// <summary>
        /// Building simple entities because of old architecture
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            ////builder.Entity<Product>().HasMany<Property>();
            builder.Entity<SimpleOrder>();
            ////builder.Entity<SimpleArticle>();

            builder.Entity<Product>()
                .HasOne(p => p.Info).WithOne(i => i.Product)
                .HasForeignKey<ProductInformation>(i => i.Id);
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<ProductInformation>().ToTable("Products");

            builder.Entity<Supplier>()
                .HasOne(p => p.Info).WithOne(i => i.Supplier)
                .HasForeignKey<SupplierInformation>(i => i.Id);
            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.Entity<SupplierInformation>().ToTable("Suppliers");

            builder.Entity<Article>()
                .HasOne(p => p.Info).WithOne(i => i.Article)
                .HasForeignKey<ArticleInformation>(i => i.Id);
            builder.Entity<Article>().ToTable("Articles");
            builder.Entity<ArticleInformation>().ToTable("Articles");

            builder.Ignore<Information>();

            base.OnModelCreating(builder);
        }
    }
}
