// <copyright file="Category.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the base category.
        /// </summary>
        /// <value>
        /// The base category.
        /// </value>
        public virtual Category BaseCategory { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Category: " + Name;
        }

        /// <summary>
        /// Determines whether this category has specified id or has parent with specified id.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified category identifier has parent; otherwise, <c>false</c>.
        /// </returns>
        public bool HasParent(Guid categoryId)
        {
            if (Id == categoryId)
            {
                return true;
            }

            if (BaseCategory == null)
            {
                return false;
            }

            return BaseCategory.HasParent(categoryId);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class CatWithSubs
    {
        /// <summary>
        /// Gets or sets the cat.
        /// </summary>
        /// <value>
        /// The cat.
        /// </value>
        public Category Cat { get; set; }

        /// <summary>
        /// Gets or sets the subs.
        /// </summary>
        /// <value>
        /// The subs.
        /// </value>
        public IEnumerable<CatWithSubs> Subs { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "CatWithSubs: " + Cat.Name;
        }
    }
}
