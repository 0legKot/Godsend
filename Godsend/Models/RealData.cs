using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Cell
    {
        public int Id { get; set; }
        public Directory Dir { get; set; }
        public Column BaseColumn { get; set; }
        //public RealData Cell { get; set; }
        public PrimitiveData Value { get; set; }
    }
}
