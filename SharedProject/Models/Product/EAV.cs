// <copyright file="EAV.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EAV<T>
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public Property Property { get; set; }

        public T Value { get; set; }
    }
}
