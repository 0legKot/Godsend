// <copyright file="Directory.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Directory
    {
        public int Id { get; set; }

        public Directory Base { get; set; }

        public string Name { get; set; }
    }
}
