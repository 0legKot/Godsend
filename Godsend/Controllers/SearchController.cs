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

    public enum SearchType
    {
        All = 0,
        Products = 1,
        Suppliers = 2,

        // Orders = 3
    }

    // todo make procedure
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private DataContext context;

        public SearchController(DataContext context)
        {
            this.context = context;
        }

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

        [HttpGet("all/{term?}")]
        public AllSearchResult FindAll(string term)
        {
            return new AllSearchResult
            {
                ProductsInfo = FindProducts(term),
                SuppliersInfo = FindSuppliers(term)
            };
        }

        [HttpGet("products/{term?}")]
        public IEnumerable<ProductInformation> FindProducts(string term)
        {
            return string.IsNullOrWhiteSpace(term)
                ? context.Products.Include(x => x.Info).Select(p => p.Info)
                : context.Products.Include(x => x.Info).Select(p => p.Info).Where(pi => pi.Name.ToLower().Contains(term.ToLower()));
        }

        [HttpGet("suppliers/{term?}")]
        public IEnumerable<SupplierInformation> FindSuppliers(string term)
        {
            var a = string.IsNullOrWhiteSpace(term)
                ? context.Suppliers
                    .Select(s => s.Info)
                    .Include(i => i.Location)
                    .ToArray()
                : context.Suppliers
                    .Select(s => s.Info)
                    .Where(si => si.Name.ToLower().Contains(term.ToLower()))
                    .Include(i => i.Location)
                    .ToArray();

            return a;
        }
    }
}
