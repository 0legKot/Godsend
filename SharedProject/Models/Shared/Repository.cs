// <copyright file="IRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="IEntity">The type of the entity.</typeparam>
    public abstract class Repository<T>
        where T : class,IEntity
    {
        protected DataContext context;
        protected ICommentHelper<T> commentHelper;

        public Repository(DataContext ctx, ISeedHelper seedHelper, ICommentHelper<T> commentHelper)
        {
            context = ctx;
            this.commentHelper = commentHelper;
            seedHelper.EnsurePopulated(ctx);
        }

        protected abstract IQueryable<T> EntitiesSource { get; }

        protected abstract IQueryable<LinkRatingEntity<T>> RatingsSource { get; }

        protected abstract void SaveChangedState(T changedEntity);

        public virtual IEnumerable<T> GetEntities(int quantity, int skip = 0)
        {
            return EntitiesSource.Skip(skip).Take(quantity);
        }

        public virtual IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0)
        {
            return GetEntities(quantity, skip).Select(e => e.EntityInfo);
        }

        public virtual T GetEntity(Guid entityId)
        {
            return EntitiesSource.FirstOrDefault(e => e.Id == entityId);
        }

        public virtual int EntitiesCount()
        {
            return EntitiesSource.Count();
        }

        public abstract Task SaveEntity(T entity);

        public abstract Task DeleteEntity(Guid entityId);

        // not used anywhere
        ////bool IsFirst(T entity);

        public virtual void Watch(T entity)
        {
            if (entity != null)
            {
                ++entity.EntityInfo.Watches;
                SaveChangedState(entity);
            }
        }

        public virtual async Task<double> SetRatingAsync(Guid entityId, string userId, int rating)
        {
            var existingRating = await RatingsSource.FirstOrDefaultAsync(link => link.UserId == userId && link.EntityId == entityId);

            if (existingRating == null)
            {
                var newRating = new LinkRatingEntity<T> { UserId = userId, Rating = rating, EntityId = entityId };
                AddAndSaveRating(newRating);
            }
            else
            {
                existingRating.Rating = rating;
                await context.SaveChangesAsync();
            }

            return await RecalcRatingsAsync(entityId);
        }

        protected void AddAndSaveRating(LinkRatingEntity<T> newRating)
        {
            context.Add(newRating);
            context.SaveChanges();
        }

        public virtual IEnumerable<LinkRatingEntity<T>> GetAllRatings(Guid entityId)
        {
            return RatingsSource.Where(lre => lre.EntityId == entityId);
        }

        public virtual int? GetUserRating(Guid entityId, string userId)
        {
            return RatingsSource.FirstOrDefault(lre => lre.EntityId == entityId && lre.UserId == userId)?.Rating;
        }

        protected virtual async Task<double> RecalcRatingsAsync(Guid entityId)
        {
            var ratingsExist = await RatingsSource.AnyAsync(link => link.EntityId == entityId);

            var avg = ratingsExist
                ? await RatingsSource
                    .Where(link => link.EntityId == entityId)
                    .Select(link => link.Rating)
                    .AverageAsync()
                : 0;

            var entity = await EntitiesSource.FirstOrDefaultAsync(p => p.Id == entityId);

            entity.EntityInfo.Rating = avg;

            SaveChangedState(entity);

            return avg;
        }

        public async Task<Guid> AddCommentAsync(Guid entityId, string userId, Guid? baseCommentId, string comment)
        {
            var newCommentId = await commentHelper.AddCommentGenericAsync<LinkCommentEntity<T>>(context, entityId, userId, baseCommentId, comment);

            await RecalcCommentsAsync(entityId);

            return newCommentId;
        }

        public async Task RecalcCommentsAsync(Guid entityId)
        {
            var commentCount = await context.Set<LinkCommentEntity<T>>().CountAsync(lce => lce.EntityId == entityId);

            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == entityId);

            entity.EntityInfo.CommentsCount = commentCount;

            await context.SaveChangesAsync();
        }

        public IEnumerable<LinkCommentEntity<T>> GetAllComments(Guid entityId) {
            var fortst = context.Set<LinkCommentEntity<T>>().Where(lra => lra.EntityId == entityId)
                .Select(x => new LinkCommentEntity<T>() { BaseComment = x.BaseComment, Comment = x.Comment, Id = x.Id, User = x.User });
            return fortst;
        }

        public async Task DeleteCommentAsync(Guid entityId, Guid commentId, string userId)
        {
            await commentHelper.DeleteCommentGenericAsync(context.Set<LinkCommentEntity<T>>(), context, entityId, commentId);

            await RecalcCommentsAsync(entityId);
        }

        public async Task EditForeignCommentAsync(Guid commentId, string newContent, string userId) {
            var comment = await context.Set<LinkCommentEntity<T>>().FirstOrDefaultAsync(lce => lce.Id == commentId);
            if (comment.UserId != userId)
            {
                throw new InvalidOperationException("Incorrect user tried to edit comment");
            }

            comment.Comment = newContent;

            await context.SaveChangesAsync();
        }

        public Task EditOwnCommentAsync(Guid commentId, string newContent, string userId) {
            throw new NotImplementedException();
        }
    }
}
