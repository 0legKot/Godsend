using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class SampleProductStorage : IProductStorage
    {
        public IEnumerable<IProduct> Products { get; set; }
    }
}
