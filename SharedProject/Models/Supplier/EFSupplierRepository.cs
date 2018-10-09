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
    public class EFSupplierRepository : SupplierRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EFSupplierRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public EFSupplierRepository(DataContext ctx, ISeedHelper seedHelper, ICommentHelper<Supplier> commentHelper)
            : base(ctx, seedHelper, commentHelper)
        {
        }

        protected override IQueryable<Supplier> EntitiesSource => context.Suppliers;

        protected override IQueryable<LinkRatingEntity<Supplier>> RatingsSource => context.LinkRatingSupplier;

        protected override void SaveChangedState(Supplier changedEntity)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes the supplier.
        /// </summary>
        /// <param name="id">The supplier identifier.</param>
        public override async Task DeleteEntity(Guid id)
        {
            Supplier dbEntry = GetEntity(id);
            if (dbEntry != null)
            {
                var quantumMagic = dbEntry.Info;

                ////context.RemoveRange(context.LinkRatingSupplier.Where(lrs => lrs.EntityId == dbEntry.Id));
                ////context.RemoveRange(context.LinkCommentSupplier.Where(lrs => lrs.SupplierId == dbEntry.Id));
                context.Suppliers.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override async Task SaveEntity(Supplier entity)
        {
            Supplier dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                context.RemoveRange(dbEntry.LinkSupplierImages);

                entity.CopyTo(dbEntry);
            }
            else
            {
                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        public override IEnumerable<ProductInformation> GetProducts(Guid supplierId)
        {
            return context.LinkProductsSuppliers.Where(x => x.SupplierId == supplierId).Select(x => x.Product.Info);
        }
    }
}
