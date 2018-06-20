using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class Property
    {
        public Guid Id { get; set; }

        public Category RelatedCategory { get; set; }

        public string Name { get; set; }
    }
}
