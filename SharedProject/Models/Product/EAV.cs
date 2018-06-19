using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class EAV
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Property Property { get; set; }
        public string Value { get; set; }
    }
}
