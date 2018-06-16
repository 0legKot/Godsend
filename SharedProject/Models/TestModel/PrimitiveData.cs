// <copyright file="PrimitiveData.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PrimitiveData : Cell
    {
        public int Id { get; set; }

        // string=any value, needed different class for every
        public string RealData { get; set; }
    }
}
