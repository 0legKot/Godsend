namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using Godsend.Models;

    public interface IProductRepository : IRepository<DiscreteProduct>
    {
        ProductWithSuppliers GetProductWithSuppliers(Guid productId);
    }
}
