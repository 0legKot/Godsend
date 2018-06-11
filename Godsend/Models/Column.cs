using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColumnType { get; set; }
        public Directory BaseClass{get; set; }

    }
}
