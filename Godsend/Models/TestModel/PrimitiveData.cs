using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class PrimitiveData : Cell
    {
        public int Id { get; set; }

        // string=any value, needed different class for every
        public string RealData { get; set; }
    }
}
