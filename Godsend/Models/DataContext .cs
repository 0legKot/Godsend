using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
               : base(options) { }
        public DbSet<IProduct> Products { get; set; }
        public DbSet<ISupplier> Suppliers { get; set; }
        public DbSet<IOrder> Orders { get; set; }
    }
}
