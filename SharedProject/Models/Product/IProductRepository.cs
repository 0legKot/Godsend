// <copyright file="IProductRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Godsend.Models;

    /// <summary>
    ///
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Gets the product with suppliers.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        ProductWithSuppliers GetProductWithSuppliers(Guid productId);

        /// <summary>
        /// Categorieses this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> Categories();

        /// <summary>
        /// Propertieses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<object> Properties(Guid id);

        ProductInfosAndCount GetProductInformationsByProductFilter(ProductFilterInfo filter);
    }
}
