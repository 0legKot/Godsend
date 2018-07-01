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
        /// <param name="productInfoId">The product information identifier.</param>
        /// <returns></returns>
        ProductWithSuppliers GetProductWithSuppliers(Guid productInfoId);

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

        /// <summary>
        /// Products the properties int.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<object> ProductPropertiesInt(Guid id);

        /// <summary>
        /// Products the properties decimal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<object> ProductPropertiesDecimal(Guid id);

        /// <summary>
        /// Products the properties string.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<object> ProductPropertiesString(Guid id);

        /// <summary>
        /// Filters the by int.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        IQueryable<ProductInformation> FilterByInt(IEnumerable<IntPropertyInfo> props, Guid orderPropertyId);

        /// <summary>
        /// Filters the by decimal.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        IQueryable<ProductInformation> FilterByDecimal(IEnumerable<DecimalPropertyInfo> props, Guid orderPropertyId);

        /// <summary>
        /// Filters the by string.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        IQueryable<ProductInformation> FilterByString(IEnumerable<StringPropertyInfo> props, Guid orderPropertyId);

        IEnumerable<ProductInformation> GetProductInformationsByFilter(FilterInfo filter,int quantity,int skip);
    }
}
