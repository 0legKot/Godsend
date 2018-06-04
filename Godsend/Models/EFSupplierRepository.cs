using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
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

        public IEnumerable<Supplier> Suppliers => context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location);

        public void DeleteSupplier(Guid supplierID)
        {
            Supplier dbEntry = GetSupplier(supplierID);
            if (dbEntry != null)
            {
                context.Suppliers.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public void SaveSupplier(Supplier supplier)
        {
            Supplier dbEntry = GetSupplier(supplier.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = supplier.Info.Name;

                // dbEntry.Status = supplier.Status;
                // ....
            }
            else
            {
                context.Add(supplier);
            }

            context.SaveChanges();
        }

        private Supplier GetSupplier(Guid supplierID)
        {
            return context.Suppliers.Include(s => s.Info).ThenInclude(i => i.Location).FirstOrDefault(s => s.Id == supplierID);
        }
    }
}
