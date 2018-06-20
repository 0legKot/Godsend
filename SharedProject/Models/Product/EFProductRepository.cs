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

    /// <summary>
    /// 
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        /// <summary>
        /// The admin user
        /// </summary>
        private const string adminUser = "Admin";
        /// <summary>
        /// The admin password
        /// </summary>
        private const string adminPassword = "Secret123$";
        private const int maxDepth = 5;

        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFProductRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="userManager">The user manager.</param>
        public EFProductRepository(DataContext ctx, UserManager<IdentityUser> userManager)
        {
            context = ctx;

            if (!context.Categories.Any())
            {
                var food = new Category { Name = "Food" };
                var fruit = new Category { Name = "Fruit", BaseCategory = food };
                var vegetables = new Category { Name = "Vegetables", BaseCategory = food };
                var berries = new Category { Name = "Berries", BaseCategory = food };
                var confectionery = new Category { Name = "Confectionery", BaseCategory = food };
                var sugarConfections = new Category { Name = "Sugar confectionery", BaseCategory = confectionery };
                var elDevices = new Category { Name = "Electronic devices" };
                var phones = new Category { Name = "Tablets and smartphones", BaseCategory = elDevices };
                var mobilePhones = new Category { Name = "Mobile phones", BaseCategory = phones };
                var beverages = new Category { Name = "Beverages", BaseCategory = food };
                var alcBeverages = new Category { Name = "Alcoholic beverages", BaseCategory = beverages };
                var nonAlcBeverages = new Category { Name = "Non-alcoholic beverages", BaseCategory = beverages };
                var juices = new Category { Name = "Juices", BaseCategory = nonAlcBeverages };
                var ciders = new Category { Name = "Ciders", BaseCategory = alcBeverages };
                var vehicles = new Category { Name = "Vehicles" };
                var cars = new Category { Name = "Cars", BaseCategory = vehicles };
                var compactMPVs = new Category { Name = "Compact MPVs", BaseCategory = cars };

                context.Categories.AddRange(food, fruit, vegetables, berries, confectionery, sugarConfections, elDevices, 
                    phones, mobilePhones, beverages, alcBeverages, nonAlcBeverages, juices, ciders, vehicles, cars, compactMPVs);

                context.SaveChanges();
            }


            if (!context.Products.Any())
            {
                Product myApple = new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple",
                        Description = "Great fruit",
                        Rating = 5,
                        Watches = 0
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Fruit")
                };
                var prop = new Property() { RelatedCategory = myApple.Category, Name = "Vitamin A" };
                context.Properties.Add(prop);
                context.LinkProductProperty.Add(new EAV() {Product = myApple,Property = prop,Value = "7" });
                //myApple.AddCharacteristic("Vitamin A","3");
                //myApple.AddCharacteristic("Vitamin B", "5");
                //myApple.AddCharacteristic("Vitamin C", "9");

                context.Products.Add(myApple);

                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Info.Name == "Potato"))
            {
                context.Products.AddRange(
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Potato",
                            Description = "The earth apple",
                            Rating = 5,
                            Watches = 4,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Vegetables")
                    },
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Orange",
                        Description = "Chinese apple",
                        Rating = 13.0 / 3,
                        Watches = 7,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Fruit")
                },
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Pineapple",
                        Description = "Cone-looking apple",
                        Rating = 1.5,
                        Watches = 0,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Fruit")
                });

                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Info.Name == "Tomato"))
            {
                context.Products.AddRange(
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Aubergine (eggplant)",
                            Description = "The mad apple",
                            Rating = Math.PI,
                            Watches = 3,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Vegetables")
                    },
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Tomato",
                            Description = "The love apple",
                            Rating = Math.E,
                            Watches = 13,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Berries")
                    },
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Peach",
                            Description = "The persian apple (not really)",
                            Rating = 4,
                            Watches = 8,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Fruit")
                    },
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Pommegranate",
                            Description = "The seedy apple",
                            Rating = 3.4,
                            Watches = 6,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Fruit")
                    },
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Melon",
                            Description = "Apple gourd",
                            Rating = 2.1,
                            Watches = 8,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Vegetables")
                    },
                    new Product
                    {
                        Info = new ProductInformation
                        {
                            Name = "Quince",
                            Description = "Apple of Cydonia",
                            Rating = 3,
                            Watches = 1,
                        },
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Fruit")
                    });

                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Info.Name == "iPhone"))
            {
                context.Products.AddRange(
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "iPhone",
                        Description = "Another kind of apple",
                        Rating = 4.99,
                        Watches = 13,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Mobile phones")
                },
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple juice",
                        Description = "Insides of an apple squeezed to death",
                        Rating = 4.3,
                        Watches = 4,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Juices")
                },
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Applejack",
                        Description = "Fermented juice of apples",
                        Rating = 4,
                        Watches = 132,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Ciders")
                },
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Apple zephyr",
                        Description = "Marshmallow made from apples",
                        Rating = 3.3,
                        Watches = 123,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Sugar confectionery")
                },
                new Product
                {
                    Info = new ProductInformation
                    {
                        Name = "Opel Zafira",
                        Description = ".",
                        Rating = 3,
                        Watches = 3,
                    },
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Compact MPVs")
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
                    Product = products[1],
                    Supplier = suppliers[0],
                    Price = 50
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<Product> Entities => GetProductsFromContext();

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <value>
        /// The entities information.
        /// </value>
        public IEnumerable<Information> EntitiesInfo => Entities.Select(p => p.Info).ToArray();

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void SaveEntity(Product entity)
        {
            Product dbEntry = GetProductsFromContext()
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

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        public void DeleteEntity(Guid infoId)
        {
            Product dbEntry = GetProductsFromContext()
               .FirstOrDefault(p => p.Info.Id == infoId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Determines whether the specified entity is first.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity is first; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFirst(Product entity)
        {
            return !context.Products.Any(p => p.Id == entity.Id);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        public Product GetEntity(Guid entityId)
        {
            return GetProductsFromContext().Include(p => p.Info)/*.Include(p=>p.CharacteristicsList)*/.FirstOrDefault(p => p.Id == entityId);
        }

        /// <summary>
        /// Watches the specified product.
        /// </summary>
        /// <param name="prod">The product.</param>
        public void Watch(Product prod)
        {
            if (prod != null)
            {
                ++prod.Info.Watches;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the product with suppliers.
        /// </summary>
        /// <param name="productInfoId">The product information identifier.</param>
        /// <returns></returns>
        public ProductWithSuppliers GetProductWithSuppliers(Guid productInfoId)
        {
            var tmp = context.LinkProductsSuppliers
                    .Include(ps => ps.Product)
                    .ThenInclude(s => s.Info)
                    //.Include(ps => ps.Product)
                    //.ThenInclude(p => p.CharacteristicsList)
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

        /// <summary>
        /// Gets the products from context.
        /// </summary>
        /// <returns></returns>
        private IQueryable<Product> GetProductsFromContext()
        {
            return context.Products.OfType<Product>().Include(p => p.Info).Include(p => p.Category)/*.Include(p => p.CharacteristicsList)*/;
        }

        public IEnumerable<Category> Categories()
        {
            var res = context.Categories.Include(c => c.BaseCategory);
            for (int i = 0; i < maxDepth; i++)
            {
                res = res.ThenInclude(c => c.BaseCategory);
            }

            return res;
        }
    }
}
