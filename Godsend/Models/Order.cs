﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public enum Status {
        Ready,
        Shipped,
        Cancelled
            //...
    }

    public abstract class Order
    {
        public Guid Id { get; }
        public IdentityUser Customer { get; set; }
        public IEnumerable<OrderPartDiscrete> DiscreteItems { get; set; }
        public IEnumerable<OrderPartWeighted> WeightedItems { get; set; }
        public DateTime Ordered { get; set; }
        public Status Status { get; set; }
        public DateTime? Done { get; set; } 
    }

    public abstract class OrderPart
    {
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
    }

    public class OrderPartDiscrete : OrderPart
    {
        public int Quantity { get; set; }
    }

    public class OrderPartWeighted : OrderPart
    {
        public double Weight { get; set; }
    }

} 


