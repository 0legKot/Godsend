using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class OrderedProduct : IProduct
    {
        public Guid Id { get; } = Guid.NewGuid();

        public ProductInformation Info { get ; set; }
        ISupplier Supplier { get; set; }

    }
}
