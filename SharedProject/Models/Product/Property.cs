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
        public virtual Category RelatedCategory { get; set; }

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

    public class IntPropertyInfo : IPropertyInfo
    {
        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public Guid PropId { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public int Left { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public int Right { get; set; }
    }

    public interface IPropertyInfo
    {
        Guid PropId { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class DecimalPropertyInfo : IPropertyInfo
    {
        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public Guid PropId { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public decimal Left { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public decimal Right { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class StringPropertyInfo : IPropertyInfo
    {
        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public Guid PropId { get; set; }

        /// <summary>
        /// Gets or sets the part.
        /// </summary>
        /// <value>
        /// The part.
        /// </value>
        public string Part { get; set; }
    }
}
