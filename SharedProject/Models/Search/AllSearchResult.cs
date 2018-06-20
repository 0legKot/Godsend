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

    /// <summary>
    ///
    /// </summary>
    public struct AllSearchResult
    {
        /// <summary>
        /// Gets or sets the products information.
        /// </summary>
        /// <value>
        /// The products information.
        /// </value>
        public IEnumerable<ProductInformation> ProductsInfo { get; set; }

        /// <summary>
        /// Gets or sets the suppliers information.
        /// </summary>
        /// <value>
        /// The suppliers information.
        /// </value>
        public IEnumerable<SupplierInformation> SuppliersInfo { get; set; }

        // public IEnumerable<Order> Orders { get; set; }
    }
}
