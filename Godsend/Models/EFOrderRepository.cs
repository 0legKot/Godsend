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
                context.Orders.Add(
                   new SimpleOrder
                   {
                       Customer = userManager.Users.FirstOrDefault(),
                       Done = new DateTime(2014,2,2),
                       Ordered = new DateTime(2013,2,3),
                       Status = Status.Ready,
                       DiscreteItems = orderPartDiscretes
                   });
                context.SaveChanges();
            }

        }
        //TODO rework

        public IEnumerable<Order> Orders => context.Orders
            .Include(x => x.Customer)
            .Include(o=>o.DiscreteItems).ThenInclude(di=>di.Product).ThenInclude(di => di.Info)
            .Include(o => o.DiscreteItems).ThenInclude(di => di.Supplier).ThenInclude(s => s.Info)
            .Include(o=>o.WeightedItems).ThenInclude(wi=>wi.Product).ThenInclude(p=>p.Info)
            .Include(o=>o.WeightedItems).ThenInclude(wi=>wi.Supplier).ThenInclude(s=>s.Info);
        
        public void SaveOrder(Order order)
        {

            Order dbEntry = GetOrder(order.Id);
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
            Order dbEntry = GetOrder(orderId);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void ChangeStatus(Guid orderId,int status)
        {
            Order dbEntry = GetOrder(orderId);
            if (dbEntry != null)
            {
                dbEntry.Status =(Status) status;
            }

            context.SaveChanges();
        }

        private Order GetOrder(Guid orderID)
        {
            return context.Orders
            .Include(x => x.Customer)
            .Include(o => o.DiscreteItems).ThenInclude(di => di.Product).ThenInclude(di => di.Info)
            .Include(o => o.DiscreteItems).ThenInclude(di => di.Supplier).ThenInclude(s => s.Info)
            .Include(o => o.WeightedItems).ThenInclude(wi => wi.Product).ThenInclude(p => p.Info)
            .Include(o => o.WeightedItems).ThenInclude(wi => wi.Supplier).ThenInclude(s => s.Info)
            .FirstOrDefault(p => p.Id == orderID);
        }
    }
}
