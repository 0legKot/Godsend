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
        [HttpGet("[action]/one")]
        public Product All(string id)
        {
            Guid tmp;
            try { tmp=Guid.Parse(id); } catch { }
            return repository.Products.FirstOrDefault(x=>x.Id==tmp);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}