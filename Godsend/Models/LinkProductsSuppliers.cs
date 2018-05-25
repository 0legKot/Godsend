using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class LinkProductsSuppliers
    {
        ISupplier Supplier { get; set; }
        IProduct Product { get; set; }
    }
}
