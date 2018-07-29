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

        /// <summary>
        /// Building simple entities because of old architecture
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SimpleOrder>();
            builder.Entity<SimpleArticle>();
            base.OnModelCreating(builder);
        }
    }
}
