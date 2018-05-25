using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class ProductInformation : IInformation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ISupplier> Suppliers { get; set; }
        public decimal Price { get; set; }
        
        public double Rating { get; set; }
        public int Watches { get; set; }
        public int Comments { get; set; }
    }
}
