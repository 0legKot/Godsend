using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public abstract class Information
    {
         public Guid Id { get; set; }
         public string Name { get; set; }
         public double Rating { get; set; }
         public int Watches { get; set; }
    }
}
