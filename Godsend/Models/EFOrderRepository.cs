using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class EFOrderRepository:IOrderRepository
    {
        private DataContext context;
        public EFOrderRepository(DataContext ctx, UserManager<IdentityUser> userManager)
        {
            ctx = context;
            if (!ctx.Orders.Any()) {
                ctx.Orders.Add(new SimpleOrder { Customer = userManager.Users.FirstOrDefault(), Done=new DateTime(), Ordered=new DateTime(), Status=Status.Ready
                  });
                ctx.SaveChanges();
            }

        }
        //TODO rework
        public IEnumerable<Order> Orders => context.Orders.Include(x => x.Customer);

        public void SaveOrder(Order order)
        {

            Order dbEntry = context.Orders.FirstOrDefault(p => p.Id == order.Id);
            if (dbEntry != null)
            {
                //TODO: implement IClonable
                dbEntry.Customer = order.Customer;
                dbEntry.Status = order.Status;
                //....
            }
            else context.Add(order);

            context.SaveChanges();
        }

        public Order DeleteOrder(Guid orderId)
        {
            Order dbEntry = context.Orders
                .FirstOrDefault(p => p.Id == orderId);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
