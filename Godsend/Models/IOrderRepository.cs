using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }

        void SaveOrder(Order order);

        Order DeleteOrder(Guid orderID);

        void ChangeStatus(Guid orderID, int status);
    }
}
