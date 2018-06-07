namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ProductController : EntityController<Product>
    {

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        //[HttpGet("[action]")]
        //public IEnumerable<Product> All()
        //{
        //    return repository.Entities;
        //}

        //[HttpDelete("[action]/{id:Guid}")]
        //public IActionResult Delete([FromBody]Guid id)
        //{
        //    try
        //    {
        //        repository.DeleteEntity(id);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPost("[action]/{id:Guid}")]
        //public IActionResult CreateOrUpdate([FromBody]Product product)
        //{
        //    try
        //    {
        //        repository.SaveEntity(product);
        //        return Ok();
        //    }
        //    catch { return BadRequest(); }
        //}

        //[HttpPatch("[action]/{id:Guid}")]
        //public IActionResult Edit([FromBody]Product product)
        //{
        //    return CreateOrUpdate(product);
        //}

        //[HttpPut("[action]/{id:Guid}")]
        //public IActionResult Create([FromBody]Product product)
        //{
        //    return CreateOrUpdate(product);
        //}

        [HttpGet("[action]/{id:Guid}")]
        public new ProductWithSuppliers Detail(Guid id)
        {
            return (repository as IProductRepository)?.GetProductWithSuppliers(id);
        }
    }
}
