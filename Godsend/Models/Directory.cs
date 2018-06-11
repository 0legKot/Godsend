using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Directory
    {
        public int Id { get; set; }
        public Directory Base { get; set; }
        public IEnumerable<Column> Columns { get; set; }
    }
}
