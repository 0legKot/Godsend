// <copyright file="PrimitiveData.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class PrimitiveData : Cell
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        // string=any value, needed different class for every
        /// <summary>
        /// Gets or sets the real data.
        /// </summary>
        /// <value>
        /// The real data.
        /// </value>
        public string RealData { get; set; }
    }
}
