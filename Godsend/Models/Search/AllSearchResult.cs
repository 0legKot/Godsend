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

    public struct AllSearchResult
    {
        public IEnumerable<ProductInformation> ProductsInfo { get; set; }

        public IEnumerable<SupplierInformation> SuppliersInfo { get; set; }

// public IEnumerable<Order> Orders { get; set; }
    }
}
