// <copyright file="IProductRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using Godsend.Models;

    public interface IProductRepository : IRepository<SimpleProduct>
    {
        ProductWithSuppliers GetProductWithSuppliers(Guid productInfoId);
    }
}
