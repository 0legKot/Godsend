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

    public class EFSupplierRepository : ISupplierRepository
    {
        private DataContext context;

        public EFSupplierRepository(DataContext ctx/*, UserManager<IdentityUser> userManager*/)
        {
            context = ctx;
            if (!context.Suppliers.Any())
            {
                context.Suppliers.Add(new SimpleSupplier()
                {
                    Info = new SupplierInformation
                    {
                        Name = "USA supply",
                        Rating = 5,
                        Watches = 3,
                        Location = new Location() { Address = "New York" }
                    }
                });
                context.Suppliers.Add(new SimpleSupplier()
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
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Apple Inc.",
                            Rating = 4.99,
                            Watches = 156,
                            Location = new Location { Address = "Cupertino, California" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Sweet Apple Acres",
                            Rating = 4.3,
                            Watches = 721,
                            Location = new Location { Address = "Equestria" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "The Somerset Cider Brandy Company",
                            Rating = 3.2,
                            Watches = 24,
                            Location = new Location { Address = "Pass Vale Farm, Burrow Hill, Kingsbury Episcopi, Martock.Somerset" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "АПЭЛ",
                            Rating = 2.1,
                            Watches = 66,
                            Location = new Location { Address = "Tolyatti" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Opel",
                            Rating = 4.1,
                            Watches = 126,
                            Location = new Location { Address = "Rüsselsheim am Main" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Dole Food Company",
                            Rating = 3.7,
                            Watches = 22000,
                            Location = new Location { Address = "Westlake Village, California" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Monsanto",
                            Rating = 4.4,
                            Watches = 623,
                            Location = new Location { Address = "800 N. Lindbergh Boulevard St.Louis" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "Nestlé S.A.",
                            Rating = 3.8,
                            Watches = 1256,
                            Location = new Location { Address = "Vevey, Vaud" }
                        }
                    },
                    new SimpleSupplier
                    {
                        Info = new SupplierInformation
                        {
                            Name = "The Coca-Cola Company",
                            Rating = 4.5,
                            Watches = 1566,
                            Location = new Location { Address = "Atlanta, Georgia" }
                        }
                    },
                    new SimpleSupplier
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

        public IEnumerable<SimpleSupplier> Entities => context.Suppliers.OfType<SimpleSupplier>().Include(s => s.Info).ThenInclude(i => i.Location);

        public IEnumerable<Information> EntitiesInfo => Entities.Select(s => s.Info).ToArray();

        public void DeleteEntity(Guid infoId)
        {
            Supplier dbEntry = GetEntityByInfoId(infoId);
            if (dbEntry != null)
            {
                context.Suppliers.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public bool IsFirst(SimpleSupplier entity)
        {
            return !context.Suppliers.Any(s => s.Id == entity.Id);
        }

        public void Watch(SimpleSupplier sup)
        {
            if (sup != null)
            {
                ++sup.Info.Watches;
                context.SaveChanges();
            }
        }

        public void SaveEntity(SimpleSupplier entity)
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

         public SimpleSupplier GetEntity(Guid entityId)
        {
            return Entities.FirstOrDefault(s => s.Id == entityId);
        }

        public SimpleSupplier GetEntityByInfoId(Guid infoId)
        {
            return Entities.FirstOrDefault(s => s.Info.Id == infoId);
        }
    }
}
