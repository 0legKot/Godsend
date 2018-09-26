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
    public abstract class ASupplierRepository : Repository<Supplier>
    {
        public abstract IEnumerable<ProductInformation> GetProducts(Guid supplierId);
    }
}
