// <copyright file="IRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="IEntity">The type of the entity.</typeparam>
    public interface IRepository<IEntity>
    {
        IEnumerable<IEntity> Entities(int quantity, int skip = 0);

        IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0);

        IEntity GetEntity(Guid entityId);

        int EntitiesCount();

        Task SaveEntity(IEntity entity);

        Task DeleteEntity(Guid entityId);

        bool IsFirst(IEntity entity);

        void Watch(IEntity entity);

        Task<double> SetRatingAsync(Guid entityId, string userId, int rating);

        Task<Guid> AddCommentAsync(Guid entityId, string userId, Guid baseCommentId, string comment);

        IEnumerable<LinkRatingEntity> GetAllRatings(Guid entityId);

        IEnumerable<LinkCommentEntity> GetAllComments(Guid entityId);

        int? GetUserRating(Guid entityId, string userId);
    }
}
