namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using Godsend.Models;

    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(Guid productID);
    }
}
