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
        }


        public IEnumerable<Supplier> Entities => context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location);

        public void DeleteEntity(Guid entityId)
        {
            Supplier dbEntry = GetEntity(entityId);
            if (dbEntry != null)
            {
                context.Suppliers.Remove(dbEntry);
                context.SaveChanges();
            }
        }


        public bool IsFirst(Supplier entity)
        {
            return !context.Suppliers.Any(s => s.Id == entity.Id);
        }

        public void SaveEntity(Supplier entity)
        {
            Supplier dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = entity.Info.Name;

                // dbEntry.Status = supplier.Status;
                // ....
            }
            else
            {
                context.Add(entity);
            }

            context.SaveChanges();
        }


         public Supplier GetEntity(Guid entityId)
        {
            return context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location).FirstOrDefault(s => s.Id == entityId);
        }

    }
}
