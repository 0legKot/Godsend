using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> Suppliers { get; }

        void SaveSupplier(Supplier supplier);

        Product DeleteSupplier(Guid supplierID);
    }
}
