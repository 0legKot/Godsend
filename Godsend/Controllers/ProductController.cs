using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    [Route("api/[controller]s")]
    public class ProductController : Controller
    {
        IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Product> All()
        {
            return repository.Products;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}