// <copyright file="Product.cs" company="Godsend Team">
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
    public class Product : IEntity
    {
        public Product()
        {
        }

        [JsonConstructor]
        public Product(ProductInformation Info)
        {
            this.Info = new ProductInformation()
            {
                Name = Info.Name,
                Description = Info.Description,
                State = Info.State
            };
        }

        public Product(string name, string description, Category category, double rating = 0, int watches = 0, ProductState state = ProductState.Normal)
        {
            this.Category = category;
            this.Info = new ProductInformation()
            {
                Name = name,
                Description = description,
                Rating = rating,
                Watches = watches,
                State = state
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [JsonIgnore]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets the category without base category
        /// </summary>
        [NotMapped]
        public Category JSONCategory => Category == null ? null : new Category() { Id = Category.Id, Name = Category.Name };

        public Guid CategoryId { get; set; }

        public virtual ProductInformation Info { get; set; }

        public virtual IEnumerable<EAV<int>> IntProps { get; set; }

        public virtual IEnumerable<EAV<string>> StringProps { get; set; }

        public virtual IEnumerable<EAV<decimal>> DecimalProps { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<LinkProductImage> LinkProductImages { get; set; }

        [NotMapped]
        public virtual IEnumerable<Image> Images
        {
            get => LinkProductImages?.Select(lpi => lpi.Image).ToList();
            set => LinkProductImages = value?.Select(i => new LinkProductImage { ImageId = i.Id, ProductId = Id }).ToList();
        }

        [JsonIgnore]
        public virtual IEnumerable<LinkProductsSuppliers> LinkProductsSuppliers { get; set; }

        [NotMapped]
        public IEnumerable<LinkProductsSuppliers.WithoutProduct> SuppliersAndPrices
        {
            get => LinkProductsSuppliers?.Select(lps => new LinkProductsSuppliers.WithoutProduct(lps))?.ToList();
            set => LinkProductsSuppliers = value.Select(link => new LinkProductsSuppliers()
            {
                Id = link.Id,
                Price = link.Price,
                SupplierId = link.SupplierInfo.Id,
                ProductId = Id
            }).ToList();
        }

        public override string ToString()
        {
            return "Product: " + Info?.Name ?? "Hello include";
        }

        public void CopyTo(Product target)
        {
            // todo state
            target.Info.Name = Info.Name;
            target.Info.Description = Info.Description;
            target.CategoryId = CategoryId;
            target.DecimalProps = DecimalProps.Select(eav => new EAV<decimal>() { ProductId = eav.ProductId, PropertyId = eav.Property.Id, Value = eav.Value }).ToList();
            target.IntProps = IntProps.Select(eav => new EAV<int>() { ProductId = eav.ProductId, PropertyId = eav.Property.Id, Value = eav.Value }).ToList();
            target.StringProps = StringProps.Select(eav => new EAV<string>() { ProductId = eav.ProductId, PropertyId = eav.Property.Id, Value = eav.Value }).ToList();
            target.LinkProductsSuppliers = LinkProductsSuppliers;
            target.Info.Preview = Info.Preview;
            target.LinkProductImages = LinkProductImages;
        }
    }

   /* public class ProductWithSuppliers
    {
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the suppliers.
        /// </summary>
        /// <value>
        /// The suppliers.
        /// </value>
        public IEnumerable<SupplierAndPrice> Suppliers { get; set; }
    }*/

    /// <summary>
    ///
    /// </summary>
    public class SupplierAndPrice
    {
        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        /// <value>
        /// The supplier.
        /// </value>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }
    }

    public class ProductInfosAndCount
    {
        public IEnumerable<ProductInformation> Infos { get; set; }

        public int Count { get; set; }
    }
}
