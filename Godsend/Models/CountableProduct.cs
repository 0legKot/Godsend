using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class CountableProduct:IProduct
    {
        public int Count { get; set; }

        public Guid Id { get; } = Guid.NewGuid();

        public ProductInformation Info { get; set; }
    }
}
