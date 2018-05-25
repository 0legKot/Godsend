using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class ProductSeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        private static DataContext db;

         ProductSeedData(DataContext context)
        {
            db = context;
        }
        public static void EnsurePopulated()
        {
            if (db.Products.Count() == 0) {
                db.Products.Add(new DiscreteProduct {
                    Info = new ProductInformation {
                        Name = "Apple",
                        Description="Great fruit",
                        Rating=5,
                        Watches=0
                    }});
                db.SaveChanges();
            } 
            
        }
    }
}
