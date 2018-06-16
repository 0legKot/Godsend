// <copyright file="Cell.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Cell
    {
        public int Id { get; set; }

        public Directory Dir { get; set; }

        public Column BaseColumn { get; set; }

        // public RealData Cell { get; set; }
        public PrimitiveData Value { get; set; }
    }
}
