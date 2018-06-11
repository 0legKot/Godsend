using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Column
    {
        public int Id { get; set; }
        public IEnumerable<Data> Cells { get; set; }
    }
}
