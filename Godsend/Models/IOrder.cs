using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public enum Status {
        Shipped=0
            //...
    }
    public interface IOrder
    {
        Guid Id { get; }
        ISupplier Supplier { get; set; }
        IdentityUser Customer { get; set; }
        IEnumerable<IProduct> Products { get; set; }
        DateTime Ordered { get; set; }
        Status Status { get; set; }
        DateTime? Done { get; set; } 
    }
}
