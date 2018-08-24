// <copyright file="SupplierInformation.cs" company="Godsend Team">
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
    public class SupplierInformation : Information
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public virtual Location Location { get; set; }

        // public IEnumerable<IProduct> Products { get; set; }

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
    }
}
