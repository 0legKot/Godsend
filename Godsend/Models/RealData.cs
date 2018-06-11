using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Data
    {
        public int Id { get; set; }
        public Data Cell { get; set; }
        public PrimitiveData RealData { get; set; }
    }
}
