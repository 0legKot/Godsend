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
        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        IEnumerable<IEntity> Entities(int quantity, int skip = 0);

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <value>
        /// The entities information.
        /// </value>
        IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0);

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        IEntity GetEntity(Guid entityId);

        IEntity GetEntityByInfoId(Guid infoId);

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void SaveEntity(IEntity entity);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        void DeleteEntity(Guid entityId);

        /// <summary>
        /// Determines whether the specified entity is first.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity is first; otherwise, <c>false</c>.
        /// </returns>
        bool IsFirst(IEntity entity);

        /// <summary>
        /// Watches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Watch(IEntity entity);
    }
}
