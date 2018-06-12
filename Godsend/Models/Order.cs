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

    public enum Status
    {
        Ready = 0,
        Shipped = 1,
        Cancelled = 2,
        Processing = 3,

            // ...
    }

    public abstract class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonIgnore]
        public IdentityUser EFCustomer { get; set; }

        [NotMapped]
        public ClientUser Customer { get => new ClientUser { Id = EFCustomer.Id, Name = EFCustomer.UserName }; }

        public IEnumerable<OrderPartProducts> Items { get; set; }

        //public IEnumerable<OrderPartWeighted> WeightedItems { get; set; }

        public DateTime Ordered { get; set; }

        public Status Status { get; set; }

        public DateTime? Done { get; set; }
    }

    public class SimpleOrder : Order
    {
    }

    public abstract class OrderPart
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [JsonIgnore]
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }

    public class OrderPartProducts : OrderPart
    {
        public int Quantity { get; set; }
        public int Multiplier { get; set; } = 1;
    }

    //public class OrderPartWeighted : OrderPart
    //{
    //    public double Weight { get; set; }
    //}
}
