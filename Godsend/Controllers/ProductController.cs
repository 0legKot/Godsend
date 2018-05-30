using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("[action]/{id}")]
        public Product Detail()
        {
            Guid tmp;
            try { tmp=Guid.Parse(Request.Path.Value.Substring(Request.Path.Value.LastIndexOf('/')+1)); } catch { }
            return repository.Products.FirstOrDefault(x=>x.Id==tmp);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}