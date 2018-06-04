namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Product> All()
        {
            return repository.Products;
        }

        [HttpGet("[action]/{id:Guid}")]
        public Product Detail(Guid id)
        {
            return repository.Products.FirstOrDefault(x => x.Id == id);
        }
    }
}
