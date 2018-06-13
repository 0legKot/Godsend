// <copyright file="Location.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;

    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Address { get; set; }
    }
}
