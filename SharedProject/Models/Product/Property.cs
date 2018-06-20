using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public enum PropertyTypes {
        Int=0,
        String=1,
        Decimal=2
    }
    public class Property
    {
        public Guid Id { get; set; }

        public Category RelatedCategory { get; set; }

        public string Name { get; set; }

        public PropertyTypes Type { get; set; }
    }
}
