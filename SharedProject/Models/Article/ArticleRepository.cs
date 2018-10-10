// <copyright file="IArticleRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ArticleRepository : Repository<Article>
    {
        public ArticleRepository(DataContext ctx, ISeedHelper seedHelper, ICommentHelper<Article> commentHelper)
            : base(ctx, seedHelper, commentHelper)
        {
        }
    }
}
