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
    public abstract class ProductRepository : Repository<Product>
    {

        public ProductRepository(DataContext ctx, ISeedHelper seedHelper, ICommentHelper<Product> commentHelper) : base(ctx, seedHelper, commentHelper)
        {
        }

        public abstract IEnumerable<Category> Categories();

        public abstract IEnumerable<object> Properties(Guid id);

        public abstract ProductInfosAndCount GetProductInformationsByProductFilter(ProductFilterInfo filter);
    }
}
