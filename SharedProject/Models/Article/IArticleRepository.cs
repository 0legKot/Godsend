// <copyright file="IArticleRepository.cs" company="Godsend Team">
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
    public interface IArticleRepository : IRepository<Article>
    {
        /// <summary>
        /// Sets the user asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task SetUserAsync(string name);
    }
}
