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
    public abstract class Supplier : IEntity
    {
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
        public SupplierInformation Info { get; set; }

        /// <summary>
        /// Gets or sets the entity information.
        /// </summary>
        /// <value>
        /// The entity information.
        /// </value>
        [JsonIgnore]
        public Information EntityInformation
        {
            get => Info;
            set => Info = value as SupplierInformation;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SimpleSupplier : Supplier
    {
    }
}
