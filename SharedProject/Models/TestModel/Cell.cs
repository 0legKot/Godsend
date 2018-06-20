// <copyright file="Cell.cs" company="Godsend Team">
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
    public class Cell
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the dir.
        /// </summary>
        /// <value>
        /// The dir.
        /// </value>
        public Directory Dir { get; set; }

        /// <summary>
        /// Gets or sets the base column.
        /// </summary>
        /// <value>
        /// The base column.
        /// </value>
        public Column BaseColumn { get; set; }

        // public RealData Cell { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public PrimitiveData Value { get; set; }
    }
}
