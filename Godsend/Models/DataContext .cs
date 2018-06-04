using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
               : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Order> Orders { get; set; }

       // public DbSet<ProductInformation> ProductInformation { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SimpleOrder>();
            builder.Entity<DiscreteProduct>();
            builder.Entity<WeightedProduct>();
            builder.Entity<SimpleSupplier>();

            // builder.Entity<ProductInformation>();
            base.OnModelCreating(builder);
        }
    }
}
