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
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Order controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        /// <summary>
        /// The repository
        /// </summary>
        private IOrderRepository repository;
        /// <summary>
        /// The product repo
        /// </summary>
        private IProductRepository prodRepo;
        /// <summary>
        /// The supplier repo
        /// </summary>
        private ISupplierRepository supRepo;
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="prodRepo">The product repo.</param>
        /// <param name="supRepo">The sup repo.</param>
        /// <param name="context">The context.</param>
        public OrderController(IOrderRepository repo, IProductRepository prodRepo, ISupplierRepository supRepo, DataContext context)
        {
            repository = repo;
            this.prodRepo = prodRepo;
            this.supRepo = supRepo;
            this.context = context;
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<Order> All()
        {
            return repository.Orders;
        }

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        [DisableCors]
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

        /// <summary>
        /// Creates the or update.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
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
                    Items = data.DiscreteItems?.Select(item => new OrderPartProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,

                        // todo validate
                        SupplierId = item.SupplierId == Guid.Empty ? supRepo.Entities.FirstOrDefault().Id : item.SupplierId,
                        Quantity = item.Quantity,
                        Multiplier = 10
                    }).ToArray(),
                    ////WeightedItems = data.WeightedItems?.Select(item => new OrderPartWeighted
                    ////{
                    ////    Id = Guid.NewGuid(),
                    ////    ProductId = item.ProductId,
                    ////    SupplierId = item.SupplierId == Guid.Empty ? supRepo.Entities.FirstOrDefault().Id : item.SupplierId,
                    ////    Weight = item.Weight
                    ////}).ToArray(),
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
        /// <summary>
        /// Details the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("[action]/{id:Guid}")]
        public Order Detail(Guid id)
        {
            return repository.Orders.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            return (repository.DeleteOrder(id) != null) ? (IActionResult)Ok() : BadRequest();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OrderFromNg
    {
        /// <summary>
        /// Gets or sets the discrete items.
        /// </summary>
        /// <value>
        /// The discrete items.
        /// </value>
        public OrderPartDiscreteNg[] DiscreteItems { get; set; }

        // public OrderPartWeightedNg[] WeightedItems { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class OrderPartNg
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public Guid SupplierId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Godsend.Controllers.OrderPartNg" />
    public class OrderPartDiscreteNg : OrderPartNg
    {
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        public int Multiplier { get; set; }
    }

    ////public class OrderPartWeightedNg : OrderPartNg
    ////{
    ////    public double Weight { get; set; }
    ////}
}
