using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Wrapper<T>
    {
        public Guid Id { get; set; }

        public T Value { get; set; }
    }
}
