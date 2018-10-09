// <copyright file="ISupplierRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class SupplierRepository : Repository<Supplier>
    {
        public SupplierRepository(DataContext ctx, ISeedHelper seedHelper, ICommentHelper<Supplier> commentHelper) : base(ctx, seedHelper, commentHelper)
        {
        }

        public abstract IEnumerable<ProductInformation> GetProducts(Guid supplierId);
    }
}
