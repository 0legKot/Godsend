using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public Category BaseCategory { get; set; }

        public string Name { get; set; }
    }
}
