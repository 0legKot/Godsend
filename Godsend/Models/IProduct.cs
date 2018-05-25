using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface IProduct
    {
     Guid Id { get;  }
    ProductInformation Info { get; set;}
    }
}
