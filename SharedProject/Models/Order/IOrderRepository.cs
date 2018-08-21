// <copyright file="IOrderRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        IEnumerable<Order> GetOrders(int quantity, int skip);

        int GetCount();

        /// <summary>
        /// Saves the order.
        /// </summary>
        /// <param name="order">The order.</param>
        Task SaveOrder(Order order);

        /// <summary>
        /// Deletes the order.
        /// </summary>
        /// <param name="orderID">The order identifier.</param>
        /// <returns></returns>
        Order DeleteOrder(Guid orderID);

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="orderID">The order identifier.</param>
        /// <param name="status">The status.</param>
        Task<Order> ChangeStatus(Guid orderID, int status);
        Order GetOrderById(Guid id);
    }
}
