namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ProductController : EntityController<SimpleProduct>
    {
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        
        [HttpGet("[action]/{id:Guid}")]
        public new ProductWithSuppliers Detail(Guid id)
        {
            var prod = (repository as IProductRepository)?.GetProductWithSuppliers(id);
            if (prod != null) repository.Watch(prod.Product as SimpleProduct);
            return prod;
        }
    }
}
