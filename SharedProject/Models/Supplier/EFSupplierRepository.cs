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
    public class EFSupplierRepository : ASupplierRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        private IRatingHelper ratingHelper;

        private ICommentHelper commentHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFSupplierRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public EFSupplierRepository(DataContext ctx, ISeedHelper seedHelper, IRatingHelper ratingHelper, ICommentHelper commentHelper)
        {
            context = ctx;
            this.ratingHelper = ratingHelper;
            this.commentHelper = commentHelper;
            seedHelper.EnsurePopulated(ctx);
        }

        protected override IQueryable<Supplier> EntitiesSource => context.Suppliers;

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

        public override async Task<double> SetRatingAsync(Guid supplierId, string userId, int rating)
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

        public override IEnumerable<LinkRatingEntity> GetAllRatings(Guid supplierId)
        {
            return context.LinkRatingSupplier.Where(lrs => lrs.EntityId == supplierId);
        }

        public override int? GetUserRating(Guid supplierId, string userId)
        {
            return context.LinkRatingSupplier.FirstOrDefault(lra => lra.EntityId == supplierId && lra.UserId == userId)?.Rating;
        }

        public override async Task<Guid> AddCommentAsync(Guid supplierId, string userId, Guid? baseCommentId, string comment)
        {
            var newCommentId = await commentHelper.AddCommentGenericAsync<LinkCommentSupplier>(context, supplierId, userId, baseCommentId, comment);

            await RecalcCommentsAsync(supplierId); // or just increment?

            return newCommentId;
        }

        public override IEnumerable<LinkCommentEntity> GetAllComments(Guid supplierId)
        {
            var fortst = context.LinkCommentSupplier.Where(lra => lra.Supplier.Id == supplierId)
                .Select(x => new LinkCommentSupplier() { BaseComment = x.BaseComment, Comment = x.Comment, Id = x.Id, User = x.User });
            return fortst;
        }

        public override async Task DeleteCommentAsync(Guid supplierId, Guid commentId, string userId)
        {
            await commentHelper.DeleteCommentGenericAsync(context.LinkCommentSupplier, context, supplierId, commentId);

            await RecalcCommentsAsync(supplierId);
        }

        public override async Task EditCommentAsync(Guid commentId, string newContent, string userId)
        {
            var comment = await context.LinkCommentSupplier.FirstOrDefaultAsync(lce => lce.Id == commentId);

            if (comment.UserId != userId) throw new InvalidOperationException("Incorrect user tried to edit comment");
            comment.Comment = newContent;

            await context.SaveChangesAsync();
        }

        public async Task RecalcCommentsAsync(Guid supplierId)
        {
            var commentCount = await context.LinkCommentSupplier.CountAsync(lce => lce.SupplierId == supplierId);

            var supplier = await context.Suppliers.FirstOrDefaultAsync(a => a.Id == supplierId);

            supplier.Info.CommentsCount = commentCount;

            await context.SaveChangesAsync();
        }

        public override IEnumerable<ProductInformation> GetProducts(Guid supplierId)
        {
            return context.LinkProductsSuppliers.Where(x => x.SupplierId == supplierId).Select(x => x.Product.Info);
        }
    }
}
