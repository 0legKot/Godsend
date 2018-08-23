// <copyright file="OrderController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

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
        private UserManager<User> userManager;
        IHubContext<NotificationHub> hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="prodRepo">The product repo.</param>
        /// <param name="supRepo">The sup repo.</param>
        /// <param name="context">The context.</param>
        public OrderController(IOrderRepository repo, IProductRepository prodRepo, ISupplierRepository supRepo,
            DataContext context, IHubContext<NotificationHub> hubContext, UserManager<User> userManager)
        {
            repository = repo;
            this.prodRepo = prodRepo;
            this.supRepo = supRepo;
            this.context = context;
            this.hubContext = hubContext;
            this.userManager = userManager;
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{page:int}/{rpp:int}")]
        public IEnumerable<Order> All(int page, int rpp)
        {
            return repository.GetOrders(rpp, (page - 1) * rpp);
        }

        [HttpGet("[action]")]
        public int Count()
        {
            return repository.GetCount();
        }

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        [DisableCors]
        [HttpPatch("[action]/{id:Guid}/{status:int}")]
        [Authorize] // todo role
        public async Task<IActionResult> ChangeStatus(Guid id, int status)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                var order = await repository.ChangeStatus(id, status);

                await hubContext.Clients.User(userId).SendAsync("Success", "Order status successfully changed");
                var orderOwnerId = order.EFCustomer.Id;

                if (orderOwnerId != userId)
                {
                    await hubContext.Clients.User(orderOwnerId).SendAsync("Info", "Your order status has changed");
                }

                return Ok();
            }
            catch
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not change order status");

                return BadRequest();
            }
        }

        /// <summary>
        /// Creates the or update.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdate([FromBody]OrderFromNg data)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByIdAsync(userId);

            try
            {
                Order o = new SimpleOrder
                {
                    EFCustomer = user,
                    Ordered = DateTime.Now,
                    Items = data.DiscreteItems?.Select(item => new OrderPartProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,

                        // todo validate
                        SupplierId = item.SupplierId == Guid.Empty ? throw new ArgumentException() : item.SupplierId,
                        Quantity = item.Quantity,
                        Multiplier = 10
                    }).ToArray(),
                    Id = Guid.NewGuid(),
                    Status = Status.Processing
                };

                await repository.SaveOrder(o);
                /* order.Id = Guid.NewGuid();
                 repository.SaveOrder(order);*/
                await hubContext.Clients.User(userId).SendAsync("Success", "Order has been created");

                return Ok(o);
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not create order");
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
            return repository.GetOrderById(id);
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
