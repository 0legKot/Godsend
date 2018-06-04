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
        private IOrderRepository repository;

        public OrderController(IOrderRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]")]
        public IEnumerable<Order> All()
        {
            return repository.Orders;
        }

        [HttpPatch("[action]/{id:Guid}/{status:int}")]
        public IActionResult ChangeStatus(Guid id, int status)
        {
            try
            {
                repository.ChangeStatus(id, status);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("[action]/{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                repository.DeleteOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]/{id:Guid}")]
        public Order Detail(Guid id)
        {
            return repository.Orders.FirstOrDefault(x => x.Id == id);
        }
    }
}