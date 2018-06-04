using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private ISupplierRepository repository;

        public SupplierController(ISupplierRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Supplier> All()
        {
            return repository.Suppliers;
        }

        [HttpGet("[action]/{id:Guid}")]
        public Supplier Detail(Guid id)
        {
            return repository.Suppliers.FirstOrDefault(x => x.Id == id);
        }
    }
}