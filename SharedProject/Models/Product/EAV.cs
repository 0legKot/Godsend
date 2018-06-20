using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class EAV<T>
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public Property Property { get; set; }

        public T Value { get; set; }
    }
}
