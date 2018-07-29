// <copyright file="ProductInformation.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Short information about product
    /// </summary>
    public class ProductInformation : Information
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        public ProductState State { get; set; }
        public ProductInformation()
        {
            
        }

        public ProductInformation(string Name) {
            this.Name = Name;
        }

        //public Product Product { get; set; }
    }

    public enum ProductState {
        Normal = 0,
        New = 1,
        Hot = 2
    }
}
