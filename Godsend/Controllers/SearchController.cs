// <copyright file="SearchController.cs" company="Godsend Team">
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
    /// Search type
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// All
        /// </summary>
        All = 0,
        /// <summary>
        /// The products
        /// </summary>
        Products = 1,
        /// <summary>
        /// The suppliers
        /// </summary>
        Suppliers = 2,

        // Orders = 3
    }

    // todo make procedure
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SearchController(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Finds the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("type/{t:int}/{term?}")]
        public AllSearchResult Find(SearchType t, string term)
        {
            switch (t)
            {
                case SearchType.All:
                    return FindAll(term);
                case SearchType.Products:
                    return new AllSearchResult { ProductsInfo = FindProducts(term) };
                case SearchType.Suppliers:
                    return new AllSearchResult { SuppliersInfo = FindSuppliers(term) };
                default:
                    return new AllSearchResult { }; // Because failing silently is better than knowing there is a mistake
            }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("all/{term?}")]
        public AllSearchResult FindAll(string term)
        {
            return new AllSearchResult
            {
                ProductsInfo = FindProducts(term),
                SuppliersInfo = FindSuppliers(term)
            };
        }

        /// <summary>
        /// Finds the products.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("products/{term?}")]
        public IEnumerable<ProductInformation> FindProducts(string term)
        {
            return string.IsNullOrWhiteSpace(term)
                ? context.Products
                    .Select(p => p.Info)
                : context.Products
                    .Select(p => p.Info)
                    .Where(pi => pi.Name.ToLower().Contains(term.ToLower()));
        }

        /// <summary>
        /// Finds the suppliers.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("suppliers/{term?}")]
        public IEnumerable<SupplierInformation> FindSuppliers(string term)
        {
            return string.IsNullOrWhiteSpace(term)
                ? context.Suppliers
                    .Select(s => s.Info)
                    .Include(i => i.Location)
                : context.Suppliers
                    .Select(s => s.Info)
                    .Where(si => si.Name.ToLower().Contains(term.ToLower()))
                    .Include(i => i.Location);
        }
    }
}
