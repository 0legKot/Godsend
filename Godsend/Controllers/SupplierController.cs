// <copyright file="SupplierController.cs" company="Godsend Team">
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

    /// <summary>
    /// Supplier controller
    /// </summary>
    /// <seealso cref="Godsend.Controllers.EntityController{Godsend.Models.SimpleSupplier}" />
    [Route("api/[controller]")]
    public class SupplierController : EntityController<Supplier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public SupplierController(ISupplierRepository repo)
        {
            repository = repo;
        }

        // [HttpGet("[action]")]
        // public IEnumerable<Supplier> All()
        // {
        //    return repository.Entities;
        // }
        // [HttpDelete("[action]/{id:Guid}")]
        // public IActionResult Delete([FromBody]Guid id)
        // {
        //    try
        //    {
        //        repository.DeleteEntity(id);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        // }

        // [HttpPost("[action]/{id:Guid}")]
        // public IActionResult CreateOrUpdate([FromBody]Supplier supplier)
        // {
        //    try
        //    {
        //        repository.SaveEntity(supplier);
        //        return Ok();
        //    }
        //    catch { return BadRequest(); }
        // }

        // [HttpPatch("[action]/{id:Guid}")]
        // public IActionResult Edit([FromBody]Supplier supplier)
        // {
        //    return CreateOrUpdate(supplier);
        // }

        // [HttpPut("[action]/{id:Guid}")]
        // public IActionResult Create([FromBody]Supplier supplier)
        // {
        //    return CreateOrUpdate(supplier);
        // }
        /// <summary>
        /// Details the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("[action]/{id:Guid}")]
        public Supplier Detail(Guid id)
        {
            var sup = repository.Entities.FirstOrDefault(x => x.Info.Id == id);
            repository.Watch(sup);
            return sup;
        }
    }
}
