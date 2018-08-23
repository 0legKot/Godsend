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
        [HttpGet("type/{t:int}/{term?}/{page:int}/{rpp:int}")]
        public AllSearchResult Find(SearchType t, string term, int page, int rpp)
        {
            switch (t)
            {
                case SearchType.All:
                    return FindAll(term, page, rpp);
                case SearchType.Products:
                    return new AllSearchResult { ProductsInfo = FindProducts(term, page, rpp) };
                case SearchType.Suppliers:
                    return new AllSearchResult { SuppliersInfo = FindSuppliers(term, page, rpp) };
                default:
                    return new AllSearchResult { }; // Because failing silently is better than knowing there is a mistake
            }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("all/{term?}/{page:int}/{rpp:int}")]
        public AllSearchResult FindAll(string term, int page, int rpp)
        {
            return new AllSearchResult
            {
                ProductsInfo = FindProducts(term, page, rpp),
                SuppliersInfo = FindSuppliers(term, page, rpp)
            };
        }

        /// <summary>
        /// Finds the products.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("products/{term?}/{page:int}/{rpp:int}")]
        public IEnumerable<ProductInformation> FindProducts(string term, int page, int rpp)
        {
            var x = string.IsNullOrWhiteSpace(term)
                ? context.Products.Include(p => p.Info)
                    .Select(p => p.Info)
                : context.Products.Include(p => p.Info)
                    .Select(p => p.Info)
                    .Where(pi => pi.Name.ToLower().Contains(term.ToLower()));
            return x.Cast<ProductInformation>();
        }

        /// <summary>
        /// Finds the suppliers.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        [HttpGet("suppliers/{term?}/{page:int}/{rpp:int}")]
        public IEnumerable<SupplierInformation> FindSuppliers(string term, int page, int rpp)
        {
            var x = string.IsNullOrWhiteSpace(term)
                ? context.Suppliers.Include(p => p.Info)
                    .Select(s => s.Info)

                //.Include(i => i.Location)
                : context.Suppliers.Include(p => p.Info)
                    .Select(s => s.Info)
                    .Where(si => si.Name.ToLower().Contains(term.ToLower()));

                   // .Include(i => i.Location);
            return x.Cast<SupplierInformation>();
        }
    }
}
