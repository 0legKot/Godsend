// <copyright file="LinkProductsSuppliers.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public class LinkProductsSuppliers
    {
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public class WithoutProduct
        {
            public Guid Id { get; set; }

            public SupplierInformation SupplierInfo { get; set; }

            public decimal Price { get; set; }

            public WithoutProduct(LinkProductsSuppliers link)
            {
                if (link != null)
                {
                    Id = link.Id;
                    SupplierInfo = link.Supplier?.Info;
                    Price = link.Price;
                }
            }
        }

        public class WithoutSupplier
        {
            public Guid Id { get; set; }

            public ProductInformation ProductInfo { get; set; }

            public decimal Price { get; set; }

            public WithoutSupplier(LinkProductsSuppliers link)
            {
                if (link != null)
                {
                    Id = link.Id;
                    ProductInfo = link.Product?.Info;
                    Price = link.Price;
                }
            }
        }

        // left in stock (gg)
    }
}
