// <copyright file="Column.cs" company="Godsend Team">
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
    public class Column
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        /// <value>
        /// The type of the column.
        /// </value>
        public string ColumnType { get; set; }

        /// <summary>
        /// Gets or sets the base class.
        /// </summary>
        /// <value>
        /// The base class.
        /// </value>
        public Directory BaseClass { get; set; }
    }
}
