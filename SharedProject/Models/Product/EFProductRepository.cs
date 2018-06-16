// <copyright file="EFProductRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class EFProductRepository : IProductRepository
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        private DataContext context;

        public EFProductRepository(DataContext ctx, UserManager<IdentityUser> userManager)
        {
            context = ctx;

            if (!context.Products.Any())
            {
                context.Products.Add(new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple",
                        Description = "Great fruit",
                        Rating = 5,
                        Watches = 0
                    },
                });
                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Info.Name == "Potato"))
            {
                context.Products.AddRange(
                    new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Potato",
                        Description = "The earth apple",
                        Rating = 5,
                        Watches = 4,
                    },
                    },
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Orange",
                        Description = "Chinese apple",
                        Rating = 13.0 / 3,
                        Watches = 7,
                    },
                },
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Pineapple",
                        Description = "Cone-looking apple",
                        Rating = 1.5,
                        Watches = 0,
                    },
                });

                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Info.Name == "Tomato"))
            {
                context.Products.AddRange(
                    new SimpleProduct
                    {
                        Info = new ProductInformation
                        {
                            Name = "Aubergine (eggplant)",
                            Description = "The mad apple",
                            Rating = Math.PI,
                            Watches = 3,
                        },
                    },
                    new SimpleProduct
                    {
                        Info = new ProductInformation
                        {
                            Name = "Tomato",
                            Description = "The love apple",
                            Rating = Math.E,
                            Watches = 13,
                        },
                    },
                    new SimpleProduct
                    {
                        Info = new ProductInformation
                        {
                            Name = "Peach",
                            Description = "The persian apple (not really)",
                            Rating = 4,
                            Watches = 8,
                        },
                    },
                    new SimpleProduct
                    {
                        Info = new ProductInformation
                        {
                            Name = "Pommegranate",
                            Description = "The seedy apple",
                            Rating = 3.4,
                            Watches = 6,
                        },
                    },
                    new SimpleProduct
                    {
                        Info = new ProductInformation
                        {
                            Name = "Melon",
                            Description = "Apple gourd",
                            Rating = 2.1,
                            Watches = 8,
                        },
                    },
                    new SimpleProduct
                    {
                        Info = new ProductInformation
                        {
                            Name = "Quince",
                            Description = "Apple of Cydonia",
                            Rating = 3,
                            Watches = 1,
                        },
                    });

                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Info.Name == "iPhone"))
            {
                context.Products.AddRange(
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "iPhone",
                        Description = "Another kind of apple",
                        Rating = 4.99,
                        Watches = 13,
                    },
                },
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple juice",
                        Description = "Insides of an apple squeezed to death",
                        Rating = 4.3,
                        Watches = 4,

                    },

                },
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Applejack",
                        Description = "Fermented juice of apples",
                        Rating = 4,
                        Watches = 132,
                    },

                },
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple zephyr",
                        Description = "Marshmallow made from apples",
                        Rating = 3.3,
                        Watches = 123,
                    },
                },
                new SimpleProduct
                {
                    Info = new ProductInformation
                    {
                        Name = "Opel Zafira",
                        Description = ".",
                        Rating = 3,
                        Watches = 3,
                    },

                });
                context.SaveChanges();
            }

            if (!context.LinkProductsSuppliers.Any())
            {
                var seedSuppliers = new EFSupplierRepository(context);

                // elementAt throws not supported exception
                var products = context.Products.ToArray();
                var suppliers = context.Suppliers.ToArray();

                for (int i = 0; i < context.Products.Count(); ++i)
                {
                    context.LinkProductsSuppliers.Add(
                        new LinkProductsSuppliers
                        {
                            Product = products[i],
                            Supplier = suppliers[i % context.Suppliers.Count()],
                            Price = (decimal)((i + 1) * 100.1)
                        });
                }

                context.LinkProductsSuppliers.Add(
                new LinkProductsSuppliers
                {
                    Product = products[0],
                    Supplier = suppliers[0],
                    Price = 50
                });
                context.SaveChanges();
            }
        }

        public IEnumerable<SimpleProduct> Entities => GetProductsFromContext();

        public IEnumerable<Information> EntitiesInfo => Entities.Select(p => p.Info).ToArray();

        public void SaveEntity(SimpleProduct entity)
        {
            SimpleProduct dbEntry = GetProductsFromContext()
                .FirstOrDefault(p => p.Id == entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = entity.Info.Name;
                dbEntry.Info.Description = entity.Info.Description;
            }
            else
            {
                ////entity.SetIds();
                context.Add(entity);
            }

            context.SaveChanges();
        }

        public void DeleteEntity(Guid infoId)
        {
            SimpleProduct dbEntry = GetProductsFromContext()
               .FirstOrDefault(p => p.Info.Id == infoId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public bool IsFirst(SimpleProduct entity)
        {
            return !context.Products.Any(p => p.Id == entity.Id);
        }

        public SimpleProduct GetEntity(Guid entityId)
        {
            return GetProductsFromContext().Include(p => p.Info).FirstOrDefault(p => p.Id == entityId);
        }

        public void Watch(SimpleProduct prod)
        {
            if (prod != null)
            {
                ++prod.Info.Watches;
                context.SaveChanges();
            }
        }

        public ProductWithSuppliers GetProductWithSuppliers(Guid productInfoId)
        {
            var tmp = context.LinkProductsSuppliers
                    .Include(ps => ps.Product)
                    .ThenInclude(s => s.Info)
                    .Include(ps => ps.Supplier)
                    .ThenInclude(s => s.Info)
                    .Include(ps => ps.Supplier)
                    .ThenInclude(x => x.Info.Location)
                    .ToArray();

            return new ProductWithSuppliers
            {
                Product = GetProductsFromContext().FirstOrDefault(p => p.Info.Id == productInfoId),
                Suppliers = tmp
                    .Where(link => link.Product.Info.Id == productInfoId)
                    .Select(link => new SupplierAndPrice { Supplier = link.Supplier, Price = link.Price })
                    .ToArray()
            };
        }

        private IQueryable<SimpleProduct> GetProductsFromContext()
        {
            return context.Products.OfType<SimpleProduct>().Include(p=>p.Info);
        }
    }
}
