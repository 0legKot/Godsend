using Godsend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend
{
    public class EFProductRepository: IProductRepository
    {
        private DataContext context;
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        public EFProductRepository(DataContext ctx, UserManager<IdentityUser> userManager)
        {
            context = ctx;

            if (!context.Products.Any())
            {
                context.Products.Add(new DiscreteProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple",
                        Description = "Great fruit",
                        Rating = 5,
                        Watches = 0
                    }
                });
                context.SaveChanges();

            };

            if (!context.Products.Any(p => p.Info.Name == "Potato"))
            {
                context.Products.AddRange(new DiscreteProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Potato",
                        Description = "The earth apple",
                        Rating = 5,
                        Watches = 4
                    }
                },
                new DiscreteProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Orange",
                        Description = "Chinese apple",
                        Rating = 13.0 / 3,
                        Watches = 7
                    }
                },
                new DiscreteProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Pineapple",
                        Description = "Cone-looking apple",
                        Rating = 1.5,
                        Watches = 0
                    }
                });

                context.SaveChanges();
            }
                
                
            
        }

        public IEnumerable<Product> Products => context.Products.Include(x => x.Info);

        public void SaveProduct(Product product)
        {
           
            Product dbEntry = context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (dbEntry != null)
            {
                //TODO: implement IClonable
                dbEntry.Info.Name = product.Info.Name;
                dbEntry.Info.Description = product.Info.Description;
                dbEntry.Info.Watches = product.Info.Watches;
                dbEntry.Info.Rating = product.Info.Rating;
            }
            else context.Add(product);
            
            context.SaveChanges();
        }

        public Product DeleteProduct(Guid productID)
        {
            Product dbEntry = context.Products
                .FirstOrDefault(p => p.Id == productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}