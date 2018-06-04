// <copyright file="AllSearchResult.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class AllSearchResult
    {
        public IEnumerable<Product> Products { get; set; } = null;

        public IEnumerable<Supplier> Suppliers { get; set; } = null;

// public IEnumerable<Order> Orders { get; set; }
    }
}
