// <copyright file="EntityController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    /// <summary>
    /// Base controller for entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public abstract class EntityController<TEntity> : Controller
        where TEntity : IEntity
    {
        /// <summary>
        /// The repository
        /// </summary>
        protected IRepository<TEntity> repository;

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="rpp">Results per page.</param>
        /// <returns></returns>
        [HttpGet("[action]/{page:int}/{rpp:int}")]
        public virtual IEnumerable<Information> All(int page, int rpp)
        {
            return repository.EntitiesInfo(rpp,(page-1)*rpp);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id:Guid}")]
        public virtual IActionResult Delete(Guid id)
        {
            try
            {
                repository.DeleteEntity(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Creates the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody]TEntity entity)
        {
            try
            {
                repository.SaveEntity(entity);
                return Ok(entity.Id);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Edits the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPatch("[action]/{id:Guid}")]
        public virtual async Task<IActionResult> EditAsync([FromBody]TEntity entity)
        {
            return await CreateOrUpdate(entity);
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("[action]/{id:Guid}")]
        public virtual async Task<IActionResult> CreateAsync([FromBody]TEntity entity)
        {
            return await CreateOrUpdate(entity);
        }

        /*[HttpGet("[action]/{id:Guid}")]
        public virtual TEntity Detail(Guid id)
        {
            return repository.GetEntity(id);
        }*/
    }
}
