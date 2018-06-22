// <copyright file="Property.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public enum PropertyTypes
    {
        Int = 0,
        String = 1,
        Decimal = 2
    }

    public class Property
    {
        public Guid Id { get; set; }

        public Category RelatedCategory { get; set; }

        public string Name { get; set; }

        public PropertyTypes Type { get; set; }
    }
}
