// <copyright file="OrderController.cs" company="Godsend Team">
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
    public class OrderController : Controller
    {
        private IOrderRepository repository;

        public OrderController(IOrderRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Order> All()
        {
            return repository.Orders;
        }

        [HttpPatch("[action]/{id:Guid}/{status:int}")]
        public IActionResult ChangeStatus(Guid id, int status)
        {
            try
            {
                repository.ChangeStatus(id, status);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("[action]/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                repository.DeleteOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]/{id:Guid}")]
        public IActionResult CreateOrUpdate([FromBody]Order order)
        {
            try
            {
                repository.SaveOrder(order);
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpPatch("[action]/{id:Guid}")]
        public IActionResult Edit([FromBody]Order order)
        {
            return CreateOrUpdate(order);
        }

        [HttpPut("[action]/{id:Guid}")]
        public IActionResult Create([FromBody]Order order)
        {
            return CreateOrUpdate(order);
        }
        [HttpGet("[action]/{id:Guid}")]
        public Order Detail(Guid id)
        {
            return repository.Orders.FirstOrDefault(x => x.Id == id);
        }
    }
}
