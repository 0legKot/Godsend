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
    public abstract class ARepository<T>
        where T : IEntity
    {
        protected abstract IQueryable<T> EntitiesSource { get; }

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

        public abstract Task<double> SetRatingAsync(Guid entityId, string userId, int rating);

        public abstract Task<Guid> AddCommentAsync(Guid entityId, string userId, Guid? baseCommentId, string comment);

        public abstract IEnumerable<LinkRatingEntity> GetAllRatings(Guid entityId);

        public abstract IEnumerable<LinkCommentEntity> GetAllComments(Guid entityId);

        public abstract int? GetUserRating(Guid entityId, string userId);

        public abstract Task DeleteCommentAsync(Guid entityId, Guid commentId, string userId);

        public abstract Task EditCommentAsync(Guid commentId, string newContent, string userId);
    }
}
