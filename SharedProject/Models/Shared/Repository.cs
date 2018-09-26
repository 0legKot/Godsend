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
        where T : IEntity
    {
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
                SaveChangedRating(existingRating);
            }

            return await RecalcRatingsAsync(entityId);
        }

        protected abstract void AddAndSaveRating(LinkRatingEntity<T> newRating);

        protected abstract void SaveChangedRating(LinkRatingEntity<T> rating);

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

        public abstract Task<Guid> AddCommentAsync(Guid entityId, string userId, Guid? baseCommentId, string comment);

        public abstract IEnumerable<LinkCommentEntity> GetAllComments(Guid entityId);

        public abstract Task DeleteCommentAsync(Guid entityId, Guid commentId, string userId);

        public abstract Task EditCommentAsync(Guid commentId, string newContent, string userId);
    }
}
