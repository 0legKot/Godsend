// <copyright file="Order.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;

    /// <summary>
    ///
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The ready
        /// </summary>
        Ready = 0,

        /// <summary>
        /// The shipped
        /// </summary>
        Shipped = 1,

        /// <summary>
        /// The cancelled
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// The processing
        /// </summary>
        Processing = 3,

            // ...
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class Order
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the ef customer.
        /// </summary>
        /// <value>
        /// The ef customer.
        /// </value>
        [JsonIgnore]
        public virtual User EFCustomer { get; set; }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        [NotMapped]
        public virtual ClientUser Customer => ClientUser.FromEFUser(EFCustomer);

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public virtual IEnumerable<OrderPartProducts> Items { get; set; }

        ////public IEnumerable<OrderPartWeighted> WeightedItems { get; set; }

        /// <summary>
        /// Gets or sets the ordered.
        /// </summary>
        /// <value>
        /// The ordered.
        /// </value>
        public DateTime Ordered { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the done.
        /// </summary>
        /// <value>
        /// The done.
        /// </value>
        public DateTime? Done { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class SimpleOrder : Order
    {
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class OrderPart
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [JsonIgnore]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        [JsonIgnore]
        public Guid SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        /// <value>
        /// The supplier.
        /// </value>
        public virtual Supplier Supplier { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class OrderPartProducts : OrderPart
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
        public int Multiplier { get; set; } = 1;
    }

    ////public class OrderPartWeighted : OrderPart
    ////{
    ////    public double Weight { get; set; }
    ////}
}
