namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ISupplierRepository
    {
        IEnumerable<Supplier> Suppliers { get; }

        void SaveSupplier(Supplier supplier);

        void DeleteSupplier(Guid supplierID);
    }
}
