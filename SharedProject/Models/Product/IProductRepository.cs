// <copyright file="IProductRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using Godsend.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Gets the product with suppliers.
        /// </summary>
        /// <param name="productInfoId">The product information identifier.</param>
        /// <returns></returns>
        ProductWithSuppliers GetProductWithSuppliers(Guid productInfoId);

        IEnumerable<Category> Categories();
        IEnumerable<object> Properties(Guid id);
        IEnumerable<object> ProductPropertiesInt(Guid id);
        IEnumerable<object> ProductPropertiesDecimal(Guid id);
        IEnumerable<object> ProductPropertiesString(Guid id);
        IEnumerable<ProductInformation> FilterByInt(Guid propId, int leftBound, int rightBound, int quantity, int skip);
        IEnumerable<ProductInformation> FilterByDecimal(Guid propId, decimal leftBound, decimal rightBound, int quantity, int skip);
    }
}
