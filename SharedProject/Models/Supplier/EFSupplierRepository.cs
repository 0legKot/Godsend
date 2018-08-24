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

        private UserManager<User> userManager;

        private IRatingHelper ratingHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFSupplierRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public EFSupplierRepository(DataContext ctx, ISeedHelper seedHelper, UserManager<User> userManager, IRatingHelper ratingHelper)
        {
            context = ctx;
            this.userManager = userManager;
            this.ratingHelper = ratingHelper;
            seedHelper.EnsurePopulated(ctx);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<Supplier> Entities(int quantity, int skip = 0) => context.Suppliers
            .Skip(skip).Take(quantity);

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <value>
        /// The entities information.
        /// </value>
        public IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0) => Entities(quantity, skip).Select(s => s.Info).ToArray();

        /// <summary>
        /// Deletes the supplier.
        /// </summary>
        /// <param name="id">The supplier identifier.</param>
        public async Task DeleteEntity(Guid id)
        {
            Supplier dbEntry = GetEntity(id);
            if (dbEntry != null)
            {
                context.RemoveRange(context.LinkRatingSupplier.Where(lrs => lrs.EntityId == dbEntry.Id));
                context.RemoveRange(context.LinkCommentSupplier.Where(lrs => lrs.SupplierId == dbEntry.Id));
                context.Suppliers.Remove(dbEntry);
                await context.SaveChangesAsync();
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
        public async Task SaveEntity(Supplier entity)
        {
            Supplier dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                dbEntry.Info.Name = entity.Info.Name;
                dbEntry.Info.Location.Address = entity.Info.Location.Address;

                // TODO: implement IClonable

                // dbEntry.Status = supplier.Status;
                // ....
            }
            else
            {
                ////entity.SetIds();
                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        public Supplier GetEntity(Guid entityId)
        {
            return context.Suppliers.FirstOrDefault(s => s.Id == entityId);
        }

        public int EntitiesCount()
        {
            return context.Suppliers.Count();
        }

        public async Task<double> SetRatingAsync(Guid supplierId, string userId, int rating)
        {
            await ratingHelper.SetRatingAsync(supplierId, userId, rating, context.LinkRatingSupplier, context);

            return await RecalcRatingsAsync(supplierId);
        }

        private async Task<double> RecalcRatingsAsync(Guid supplierId)
        {
            var avg = await ratingHelper.CalculateAverageAsync(context.LinkRatingSupplier, supplierId);

            var supplier = await context.Suppliers.FirstOrDefaultAsync(p => p.Id == supplierId);
            supplier.Info.Rating = avg;

            await context.SaveChangesAsync();

            return avg;
        }

        public IEnumerable<LinkRatingEntity> GetAllRatings(Guid supplierId)
        {
            return context.LinkRatingSupplier.Where(lrs => lrs.EntityId == supplierId);
        }

        public int? GetUserRating(Guid supplierId, string userId)
        {
            return context.LinkRatingSupplier.FirstOrDefault(lra => lra.EntityId == supplierId && lra.UserId == userId)?.Rating;
        }

        public async Task<Guid> AddCommentAsync(Guid supplierId, string userId, Guid baseCommentId, string comment)
        {
            var newComment = new LinkCommentSupplier
            {
                SupplierId = supplierId,
                UserId = userId,
                BaseComment = new LinkCommentEntity() { Id = baseCommentId },
                Comment = comment
            };
            context.Add(newComment);

            await context.SaveChangesAsync();

            return newComment.Id;
        }

        public IEnumerable<LinkCommentEntity> GetAllComments(Guid supplierId)
        {
            return context.LinkCommentSupplier.Where(lrs => lrs.SupplierId == supplierId);
        }
    }
}
