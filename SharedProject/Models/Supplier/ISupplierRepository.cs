// <copyright file="ISupplierRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface ISupplierRepository : IRepository<Supplier>
    {
        IEnumerable<ProductInformation> GetProducts(Guid supplierId);
    }
}
