// <copyright file="Supplier.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    ///
    /// </summary>
    public class Supplier : IEntity
    {
        public Supplier()
        { }
        [JsonConstructor]
        public Supplier(SupplierInformation Info)
        {
            this.Info = new SupplierInformation();
            (this.Info as SupplierInformation).Name = Info.Name;
            (this.Info as SupplierInformation).Location = Info.Location;
            
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
       // public SupplierInformation Info { get; set; }

        /// <summary>
        /// Gets or sets the entity information.
        /// </summary>
        /// <value>
        /// The entity information.
        /// </value>

        public virtual Information Info { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class SupplierWithProducts
    {
        /// <summary>
        /// Gets or sets the supp.
        /// </summary>
        /// <value>
        /// The supp.
        /// </value>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        /// <value>
        /// The Products.
        /// </value>
        public IEnumerable<ProductAndPrice> Products { get; set; }
    }

    public class ProductAndPrice
    {
        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        /// <value>
        /// The Product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }
    }
}
