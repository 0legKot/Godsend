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

    public abstract class EntityController<TEntity> : Controller
        where TEntity : IEntity
    {
        protected IRepository<TEntity> repository;

        [HttpGet("[action]")]
        public virtual IEnumerable<Information> All()
        {
            return repository.EntitiesInfo;
        }

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

        [HttpPost("[action]")]
        public virtual Task<IActionResult> CreateOrUpdate([FromBody]TEntity entity)
        {
            try
            {
                repository.SaveEntity(entity);
                return new Task<IActionResult>(() => Ok(entity.Id));
            }
            catch
            {
                return new Task<IActionResult>(() => BadRequest());
            }
        }

        [HttpPatch("[action]/{id:Guid}")]
        public virtual async Task<IActionResult> EditAsync([FromBody]TEntity entity)
        {
            return await CreateOrUpdate(entity);
        }

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
