using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Godsend.Controllers
{
    // todo make procedure
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private DataContext context;

        public SearchController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("type/{t:int}/{term}")]
        public AllSearchResult Find(SearchType t, string term)
        {
            switch (t)
            {
                case SearchType.All:
                    return FindAll(term);
                case SearchType.Products:
                    return new AllSearchResult { Products = FindProducts(term) };
                case SearchType.Suppliers:
                    return new AllSearchResult { Suppliers = FindSuppliers(term) };
                default:
                    throw new ArgumentException("Invalid type");
            }
        }

        [HttpGet("all/{term}")]
        public AllSearchResult FindAll(string term)
        {
            return new AllSearchResult
            {
                Products = FindProducts(term),
                Suppliers = FindSuppliers(term)
            };
        }

        [HttpGet("products/{term}")]
        public IEnumerable<Product> FindProducts(string term)
        {
            return context.Products.Include(x=>x.Info).Where(p => p.Info.Name.ToLower().Contains(term.ToLower()));
        }

        [HttpGet("suppliers/{term}")]
        public IEnumerable<Supplier> FindSuppliers(string term)
        {
            return context.Suppliers.Include(x => x.Info).Where(s => s.Info.Name.ToLower().Contains(term.ToLower()));
        }
    }

    public enum SearchType
    {
        All = 0,
        Products = 1,
        Suppliers = 2,
//        Orders = 3
    }

    public class AllSearchResult
    {
        public IEnumerable<Product> Products { get; set; } = null;
        public IEnumerable<Supplier> Suppliers { get; set; } = null;
//        public IEnumerable<Order> Orders { get; set; }
    }
}
