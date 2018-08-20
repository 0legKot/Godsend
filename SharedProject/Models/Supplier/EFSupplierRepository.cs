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
        public EFSupplierRepository(DataContext ctx, ISeedHelper seedHelper)
        {
            context = ctx;

            seedHelper.EnsurePopulated(ctx);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<Supplier> Entities(int quantity, int skip = 0) => context.Suppliers
            //.Include("Info")
           // .Include(s =>s.Info ).ThenInclude(i => (i as SupplierInformation).Location)
            
            .Skip(skip).Take(quantity);

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <value>
        /// The entities information.
        /// </value>
        public IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0) => Entities(quantity, skip).Select(s => s.Info).ToArray();

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
            Supplier dbEntry = GetEntityByInfoId(entity.Info.Id);
            if (dbEntry != null)
            {
                dbEntry.Info.Name = entity.Info.Name;
                //(dbEntry.Info as SupplierInformation).Location.Address = (entity.Info as SupplierInformation).Location.Address;
                // TODO: implement IClonable

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
            return context.Suppliers.Include(s => (s.Info as SupplierInformation)).ThenInclude(i => i.Location).FirstOrDefault(s => s.Id == entityId);
        }

        public class Hell:Supplier {
            public override Information Info { get => base.Info; set => base.Info = value as SupplierInformation; }
        }

        /// <summary>
        /// Gets the entity by information identifier.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        /// <returns></returns>
        public Supplier GetEntityByInfoId(Guid infoId)
        {
            //TODO: fix includes
            Supplier x = context.Suppliers.Include(s => s.Info)
                .FirstOrDefault(s => s.Info.Id == infoId);
            if (x == null) return null;
            //SupplierInformation supplierInformation1 = context.Set<SupplierInformation>().FirstOrDefault(z => z.Id == x.Info.Id);
            //Location supplierInformation = context.Set<SupplierInformation>().FirstOrDefault(z => z.Id == x.Info.Id).Location;
            Location loc = context.Set<Location>().FirstOrDefault();
            (x.Info as SupplierInformation).Location = loc;
            var tst = (x.Info as SupplierInformation).Location;
            return x;
        }

        public int EntitiesCount()
        {
            return context.Suppliers.Count();
        }
    }
}
