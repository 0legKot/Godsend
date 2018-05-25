using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class SupplierInformation:IInformation
    {
        public string Name { get; set ; }
        public double Rating { get ; set ; }
        public int Watches { get ; set ; }
        public int Comments { get ; set ; }
        Location Location { get; set; }
        //public IEnumerable<IProduct> Products { get; set; }
    }
}
