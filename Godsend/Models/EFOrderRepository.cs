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
            context = ctx;
            if (!context.Orders.Any()) {
                IList<OrderPartDiscrete> orderPartDiscretes = new List<OrderPartDiscrete>();
                foreach (var p in ctx.Products.Include(p=>p.Info).Where(p => typeof(DiscreteProduct) == p.GetType()))
                    orderPartDiscretes.Add(new OrderPartDiscrete { Quantity=p.Info.Watches*5,Product=p });
                    context.Orders.Add(
                    new SimpleOrder {
                        Customer = userManager.Users.FirstOrDefault(),
                        Done =new DateTime(1000),
                        Ordered =new DateTime(100), 
                        Status =Status.Ready,
                        DiscreteItems =orderPartDiscretes
                    });
                context.SaveChanges();
            }

        }
        //TODO rework
        //not full is returned
        public IEnumerable<Order> Orders => context.Orders.Include(x => x.Customer).Include(o=>o.DiscreteItems).ThenInclude(di=>di.Product).ThenInclude(di => di.Info).Include(o=>o.WeightedItems);

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
