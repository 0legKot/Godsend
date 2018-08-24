// <copyright file="EFOrderRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///
    /// </summary>
    public class EFOrderRepository : IOrderRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFOrderRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="userManager">The user manager.</param>
        public EFOrderRepository(DataContext ctx, UserManager<User> userManager, ISeedHelper seedHelper)
        {
            context = ctx;

            seedHelper.EnsurePopulated(ctx);
        }

        // TODO rework

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public IEnumerable<Order> GetOrders(int quantity, int skip = 0) =>
            this.context.Orders.Skip(skip).Take(quantity);

        /// <summary>
        /// Saves the order.
        /// </summary>
        /// <param name="order">The order.</param>
        public async Task SaveOrder(Order order)
        {
            Order dbEntry = GetOrder(order.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.EFCustomer = order.EFCustomer;
                dbEntry.Status = order.Status;

                // ....
            }
            else
            {
                context.Add(order);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        public Order DeleteOrder(Guid orderId)
        {
            Order dbEntry = GetOrder(orderId);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="status">The status.</param>
        public async Task<Order> ChangeStatus(Guid orderId, int status)
        {
            Order dbEntry = GetOrder(orderId);
            if (dbEntry != null)
            {
                dbEntry.Status = (Status)status;
                if ((Status)status == Status.Shipped)
                {
                    dbEntry.Done = DateTime.Now;
                }
            }

            await context.SaveChangesAsync();

            return dbEntry;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <param name="orderID">The order identifier.</param>
        /// <returns></returns>
        private Order GetOrder(Guid orderID)
        {
            return context.Orders.FirstOrDefault(p => p.Id == orderID);
        }

        public int GetCount()
        {
            return context.Orders.Count();
        }

        public Order GetOrderById(Guid id)
        {
            return this.context.Orders.FirstOrDefault(o => o.Id == id);
        }
    }
}
