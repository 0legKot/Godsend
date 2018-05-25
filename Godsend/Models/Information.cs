using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface  IInformation
    {
         string Name { get; set; }
         double Rating { get; set; }
         int Watches { get; set; }
         int Comments { get; set; }
    }
}
