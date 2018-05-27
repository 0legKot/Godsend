using Godsend.Models;
using System;
using System.Collections.Generic;

namespace Godsend
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(Guid productID);
    }
}