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

    [Route("api/[controller]")]
    public class SupplierController : EntityController<Supplier>
    {

        public SupplierController(ISupplierRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Supplier> All()
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
        public IActionResult CreateOrUpdate([FromBody]Supplier supplier)
        {
            try
            {
                repository.SaveEntity(supplier);
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpPatch("[action]/{id:Guid}")]
        public IActionResult Edit([FromBody]Supplier supplier)
        {
            return CreateOrUpdate(supplier);
        }

        [HttpPut("[action]/{id:Guid}")]
        public IActionResult Create([FromBody]Supplier supplier)
        {
            return CreateOrUpdate(supplier);
        }

        [HttpGet("[action]/{id:Guid}")]
        public Supplier Detail(Guid id)
        {
            return repository.Entities.FirstOrDefault(x => x.Id == id);
        }
    }
}
