// <copyright file="IArticleRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IArticleRepository : IRepository<Article>
    {
        Task SetUserAsync(string name);
    }
}
