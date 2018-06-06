using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    //TODO
    public abstract class EntityController<IEntity> : Controller
    {
        protected IRepository<IEntity> repository;
        [HttpGet("[action]")]
        public IEnumerable<IEntity> All()
        {
            return repository.Entities;
        }

        [HttpDelete("[action]/{id:Guid}")]
        public IActionResult Delete([FromBody]Guid id)
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

        [HttpPost("[action]/{id:Guid}")]
        public IActionResult CreateOrUpdate([FromBody]IEntity entity)
        {
            try
            {
                repository.SaveEntity(entity);
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpPatch("[action]/{id:Guid}")]
        public IActionResult Edit([FromBody]IEntity entity)
        {
            return CreateOrUpdate(entity);
        }

        [HttpPut("[action]/{id:Guid}")]
        public IActionResult Create([FromBody]IEntity entity)
        {
            return CreateOrUpdate(entity);
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEntity Detail(Guid id)
        {
            return repository.GetEntity(id);
        }
    }
}