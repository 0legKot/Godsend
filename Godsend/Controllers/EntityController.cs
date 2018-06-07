using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Godsend.Controllers
{
    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    //public class GenericControllerNameAttribute : Attribute, IControllerModelConvention
    //{
    //    public void Apply(ControllerModel controller)
    //    {
    //        if (controller.ControllerType.GetGenericTypeDefinition() == typeof(EntityController<>))
    //        {
    //            var entityType = controller.ControllerType.GenericTypeArguments[0];
    //            controller.ControllerName = entityType.Name;
    //        }
    //    }
    //}

    //[GenericControllerName]
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