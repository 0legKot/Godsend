// <copyright file="EFSupplierRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// 
    /// </summary>
    public class EFSupplierRepository : ISupplierRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFSupplierRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public EFSupplierRepository(DataContext ctx/*, UserManager<IdentityUser> userManager*/)
        {
            context = ctx;
            if (!context.Suppliers.Any())
            {
                context.Suppliers.Add(new Supplier()
                {
                    Info = new SupplierInformation
                    {
                        Name = "USA supply",
                        Rating = 5,
                        Watches = 3,
                        Location = new Location() { Address = "New York" }
                    }
                });
                context.Suppliers.Add(new Supplier()
                {
                    Info = new SupplierInformation
                    {
                        Name = "Russia supply",
                        Rating = 0,
                        Watches = 100,
                        Location = new Location() { Address = "Moscow" }
                    }
                });
                context.SaveChanges();
            }

            if (!context.Suppliers.Any(s => s.Info.Name == "Apple Inc."))
            {
                context.Suppliers.AddRange(
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Apple Inc.",
                            Rating = 4.99,
                            Watches = 156,
                            Location = new Location { Address = "Cupertino, California" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Sweet Apple Acres",
                            Rating = 4.3,
                            Watches = 721,
                            Location = new Location { Address = "Equestria" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "The Somerset Cider Brandy Company",
                            Rating = 3.2,
                            Watches = 24,
                            Location = new Location { Address = "Pass Vale Farm, Burrow Hill, Kingsbury Episcopi, Martock.Somerset" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "АПЭЛ",
                            Rating = 2.1,
                            Watches = 66,
                            Location = new Location { Address = "Tolyatti" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Opel",
                            Rating = 4.1,
                            Watches = 126,
                            Location = new Location { Address = "Rüsselsheim am Main" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Dole Food Company",
                            Rating = 3.7,
                            Watches = 22000,
                            Location = new Location { Address = "Westlake Village, California" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Monsanto",
                            Rating = 4.4,
                            Watches = 623,
                            Location = new Location { Address = "800 N. Lindbergh Boulevard St.Louis" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Nestlé S.A.",
                            Rating = 3.8,
                            Watches = 1256,
                            Location = new Location { Address = "Vevey, Vaud" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "The Coca-Cola Company",
                            Rating = 4.5,
                            Watches = 1566,
                            Location = new Location { Address = "Atlanta, Georgia" }
                        }
                    },
                    new Supplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "中国粮油食品（集团）有限公司",
                            Rating = 4.6,
                            Watches = 666,
                            Location = new Location { Address = "Beijing" }
                        }
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
        public IEnumerable<Supplier> Entities(int quantity, int skip = 0)=> context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location).Take(quantity).Skip(skip);

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <value>
        /// The entities information.
        /// </value>
        public IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0)=> Entities(quantity,skip).Select(s => s.Info).ToArray();

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        public void DeleteEntity(Guid infoId)
        {
            Supplier dbEntry = GetEntityByInfoId(infoId);
            if (dbEntry != null)
            {
                context.Suppliers.Remove(dbEntry);
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
        public bool IsFirst(Supplier entity)
        {
            return !context.Suppliers.Any(s => s.Id == entity.Id);
        }

        /// <summary>
        /// Watches the specified sup.
        /// </summary>
        /// <param name="sup">The sup.</param>
        public void Watch(Supplier sup)
        {
            if (sup != null)
            {
                ++sup.Info.Watches;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void SaveEntity(Supplier entity)
        {
            Supplier dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = entity.Info.Name;
                dbEntry.Info.Location.Address = entity.Info.Location.Address;

                // dbEntry.Status = supplier.Status;
                // ....
            }
            else
            {
                ////entity.SetIds();
                context.Add(entity);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        public Supplier GetEntity(Guid entityId)
        {
            return context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location).FirstOrDefault(s => s.Id == entityId);
        }

        /// <summary>
        /// Gets the entity by information identifier.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        /// <returns></returns>
        public Supplier GetEntityByInfoId(Guid infoId)
        {
            return context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location).FirstOrDefault(s => s.Info.Id == infoId);
        }
    }
}
