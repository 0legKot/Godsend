// <copyright file="Supplier.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    ///
    /// </summary>
    public class Supplier : IEntity
    {
        public Supplier()
        {
        }

        [JsonConstructor]
        public Supplier(SupplierInformation Info)
        {
            this.Info = new SupplierInformation()
            {
                Name = Info.Name,
                Location = Info.Location
            };
        }

        public Supplier(string name, string address, double rating = 0, int watches = 0)
        {
            this.Info = new SupplierInformation()
            {
                Name = name,
                Rating = rating,
                Watches = watches,
                Location = new Location()
                {
                    Address = address
                }
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        public virtual SupplierInformation Info { get; set; }

        public void CopyTo(Supplier target)
        {
            target.Info.Location.Address = Info.Location.Address;
            target.Info.Name = Info.Name;
        }
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
