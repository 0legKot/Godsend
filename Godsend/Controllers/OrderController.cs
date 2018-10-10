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
        private IOrderRepository repository;
        private ProductRepository prodRepo;
        private SupplierRepository supRepo;
        private DataContext context;
        private UserManager<User> userManager;
        IHubContext<NotificationHub> hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        public OrderController(
            IOrderRepository repo,
            ProductRepository prodRepo,
            SupplierRepository supRepo,
            DataContext context,
            IHubContext<NotificationHub> hubContext,
            UserManager<User> userManager)
        {
            repository = repo;
            this.prodRepo = prodRepo;
            this.supRepo = supRepo;
            this.context = context;
            this.hubContext = hubContext;
            this.userManager = userManager;
        }

        /// <param name="rpp">
        /// Results per page
        /// </param>
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
        [Authorize(Roles = "Administrator,Moderator,Supplier")]
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
                await hubContext.Clients.User(userId).SendAsync("Success", "Order has been created");

                return Ok(o);
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not create order");
                return BadRequest();
            }
        }

        [HttpGet("[action]/{id:Guid}")]
        [Authorize]
        public Order Detail(Guid id)
        {
            return repository.GetOrderById(id);
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpDelete("[action]/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            return (repository.DeleteOrder(id) != null) ? (IActionResult)Ok() : BadRequest();
        }
    }

    /// <seealso cref="Godsend.Controllers.OrderPartNg" />
    public class OrderPartDiscreteNg : OrderPartNg
    {
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        public int Multiplier { get; set; }
    }
}
