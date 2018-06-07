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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private IProductRepository prodRepo;
        private ISupplierRepository supRepo;
        private DataContext context;

        public OrderController(IOrderRepository repo, IProductRepository prodRepo, ISupplierRepository supRepo, DataContext context)
        {
            repository = repo;
            this.prodRepo = prodRepo;
            this.supRepo = supRepo;
            this.context = context;
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

        [HttpPost("[action]")]
       // public IActionResult CreateOrUpdate([FromBody]Newtonsoft.Json.Linq.JToken jdata)
        public IActionResult CreateOrUpdate([FromBody]OrderFromNg data)
        {
            try
            {
                Order o = new SimpleOrder
                {
                    EFCustomer = context.Users.FirstOrDefault(),
                    Ordered = DateTime.Now,
                    DiscreteItems = data.DiscreteItems?.Select(item => new OrderPartDiscrete
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        SupplierId = item.SupplierId == Guid.Empty ? supRepo.Entities.FirstOrDefault().Id : item.SupplierId,
                        Quantity = item.Quantity
                    }).ToArray(),
                    WeightedItems = data.WeightedItems?.Select(item => new OrderPartWeighted
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        SupplierId = item.SupplierId == Guid.Empty ? supRepo.Entities.FirstOrDefault().Id : item.SupplierId,
                        Weight = item.Weight
                    }).ToArray(),
                    Id = Guid.NewGuid(),
                    Status = Status.Processing
                };

                repository.SaveOrder(o);
               /* order.Id = Guid.NewGuid();
                repository.SaveOrder(order);*/
                return Ok(o);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

       /* [HttpPatch("[action]/{id:Guid}")]
        public IActionResult Edit([FromBody]OrderFromNg order)
        {
            return CreateOrUpdate(order);
        }

        [HttpPut("[action]/{id:Guid}")]
        public IActionResult Create([FromBody]OrderFromNg order)
        {
            return CreateOrUpdate(order);
        }*/
        [HttpGet("[action]/{id:Guid}")]
        public Order Detail(Guid id)
        {
            return repository.Orders.FirstOrDefault(x => x.Id == id);
        }

        [HttpDelete("[action]/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            return (repository.DeleteOrder(id) != null) ? (IActionResult)Ok() : BadRequest();
        }
    }

    public class OrderFromNg
    {
        public OrderPartDiscreteNg[] DiscreteItems { get; set; }

        public OrderPartWeightedNg[] WeightedItems { get; set; }
    }

    public abstract class OrderPartNg
    {
        public Guid ProductId { get; set; }

        public Guid SupplierId { get; set; }
    }

    public class OrderPartDiscreteNg : OrderPartNg
    {
        public int Quantity { get; set; }
    }

    public class OrderPartWeightedNg : OrderPartNg
    {
        public double Weight { get; set; }
    }
}
