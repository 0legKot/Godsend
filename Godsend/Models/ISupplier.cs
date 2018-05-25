using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface ISupplier
    {
        Guid Id { get; set; }
        SupplierInformation Info { get; set; }

    }
}
