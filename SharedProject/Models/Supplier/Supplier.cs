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
        public Supplier(SupplierInformation info)
        {
            Info = new SupplierInformation()
            {
                Name = info.Name,
                Location = info.Location,
                Watches = info.Watches,
                Rating = info.Rating,
                Preview = info.Preview
            };
        }

        public Supplier(string name, string address, double rating = 0, int watches = 0)
        {
            Info = new SupplierInformation()
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

        public virtual Information EntityInfo => Info;

        [JsonIgnore]
        public virtual IEnumerable<LinkSupplierImage> LinkSupplierImages { get; set; }

        [NotMapped]
        public virtual IEnumerable<Image> Images
        {
            get => LinkSupplierImages?.Select(lsi => lsi.Image).ToList();
            set => LinkSupplierImages = value?.Select(i => new LinkSupplierImage { ImageId = i.Id, SupplierId = Id }).ToList();
        }

        /// <summary>
        /// Copies editable properties to target
        /// </summary>
        /// <param name="target">The target.</param>
        public void CopyTo(Supplier target)
        {
            if (target.Info == null)
            {
                target.Info = new SupplierInformation();
            }

            if (target.Info.Location == null)
            {
                target.Info.Location = new Location();
            }

            target.Info.Location.Address = Info?.Location?.Address;
            target.Info.Name = Info.Name;
            target.LinkSupplierImages = LinkSupplierImages;
        }

        public void CloneTo(Supplier target)
        {
            CopyTo(target);
            target.Id = Id;
            target.Info.Rating = Info.Rating;
            target.Info.Watches = Info.Watches;
        }

        [JsonIgnore]
        public virtual IEnumerable<LinkProductsSuppliers> LinkProductsSuppliers { get; set; }

        [NotMapped]
        public IEnumerable<LinkProductsSuppliers.WithoutSupplier> ProductsAndPrices
        {
            get => LinkProductsSuppliers?.Select(lps => new LinkProductsSuppliers.WithoutSupplier(lps)).ToList();
            set => LinkProductsSuppliers = value.Select(link => new LinkProductsSuppliers()
            {
                Id = link.Id,
                Price = link.Price,
                SupplierId = Id,
                ProductId = link.ProductInfo.Id
            }).ToList();
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
