﻿// <copyright file="DataContext.cs" company="Godsend Team">
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

    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
               : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Cell> Values { get; set; }

        public DbSet<ImagePaths> ImagePathsTable { get; set; }

        public DbSet<LinkProductsSuppliers> LinkProductsSuppliers { get; set; }

        ////public DbSet<ProductInformation> ProductInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
