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
        Ready = 0,
        Shipped = 1,
        Cancelled = 2,
        Processing = 3,

            // ...
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class Order
    {

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

        public virtual IEnumerable<OrderPartProducts> Items { get; set; }

        public DateTime Ordered { get; set; }

        public Status Status { get; set; }

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

        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ProductId { get; set; }


        public virtual ProductInformation Product { get; set; }

        [JsonIgnore]
        public Guid SupplierId { get; set; }

        public virtual SupplierInformation Supplier { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class OrderPartProducts : OrderPart
    {
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        public int Multiplier { get; set; } = 1;
    }
}
