// <copyright file="Property.cs" company="Godsend Team">
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
    public enum PropertyTypes
    {
        /// <summary>
        /// The int
        /// </summary>
        Int = 0,
        /// <summary>
        /// The string
        /// </summary>
        String = 1,
        /// <summary>
        /// The decimal
        /// </summary>
        Decimal = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the related category.
        /// </summary>
        /// <value>
        /// The related category.
        /// </value>
        public Category RelatedCategory { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public PropertyTypes Type { get; set; }

        public override string ToString()
        {
            return "Property: " + Name;
        }
    }
}
