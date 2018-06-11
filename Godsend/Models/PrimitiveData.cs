using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class PrimitiveData : Data
    {
        public int Id { get; set; }
        public ValueType RealData { get; set; }
    }
}
