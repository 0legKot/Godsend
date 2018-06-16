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

    public abstract class Supplier : IEntity
    {
        public Guid Id { get; set; }

        public SupplierInformation Info { get; set; }

        [JsonIgnore]
        public Information EntityInformation
        {
            get => Info;
            set => Info = value as SupplierInformation;
        }
    }

    public class SimpleSupplier : Supplier
    {
    }
}
