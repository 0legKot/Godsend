using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class RealData
    {
        public int Id { get; set; }
        public Column BaseColumn { get; set; }
        public RealData Cell { get; set; }
        public PrimitiveData Value { get; set; }
    }
}
