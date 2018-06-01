using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godsend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        IOrderRepository repository;
        public OrderController(IOrderRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Order> All()
        {
            return repository.Orders;
        }

        [HttpGet("[action]/{id:Guid}")]
        public Order Detail(Guid id)
        {
            return repository.Orders.FirstOrDefault(x => x.Id == id);
        }
    }
}