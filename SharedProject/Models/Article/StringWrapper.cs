// <copyright file="StringWrapper.cs" company="Godsend Team">
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
    public class StringWrapper
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="StringWrapper"/>.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator StringWrapper(string s)
        {
            return new StringWrapper() { Value = s };
        }
    }
}
