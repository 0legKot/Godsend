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
        All = 0,
        Products = 1,
        Suppliers = 2,

        // Orders = 3
    }

    // todo make procedure ?????

    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private DataContext context;

        public SearchController(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Finds the specified entities.
        /// </summary>
        /// <param name="term">User input.</param>
        /// <returns></returns>
        [HttpGet("type/{t:int}/{term?}/{page:int}/{rpp:int}")]
        public AllSearchResult Find(SearchType searchType, string term, int page, int rpp)
        {
            switch (searchType)
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
        /// Finds all entities.
        /// </summary>
        /// <param name="term">User input.</param>
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
        /// <param name="term">User input.</param>
        [HttpGet("products/{term?}/{page:int}/{rpp:int}")]
        public IEnumerable<ProductInformation> FindProducts(string term, int page, int rpp)
        {
            var x = string.IsNullOrWhiteSpace(term)
                ? context.Products.Select(p => p.Info)
                : context.Products
                    .Select(p => p.Info)
                    .Where(pi => pi.Name.ToLower().Contains(term.ToLower()));
            return x.Cast<ProductInformation>();
        }

        /// <summary>
        /// Finds the suppliers.
        /// </summary>
        /// <param name="term">User input.</param>
        [HttpGet("suppliers/{term?}/{page:int}/{rpp:int}")]
        public IEnumerable<SupplierInformation> FindSuppliers(string term, int page, int rpp)
        {
            var x = string.IsNullOrWhiteSpace(term)
                ? context.Suppliers.Select(s => s.Info)
                : context.Suppliers
                    .Select(s => s.Info)
                    .Where(si => si.Name.ToLower().Contains(term.ToLower()));
            return x.Cast<SupplierInformation>();
        }
    }
}
