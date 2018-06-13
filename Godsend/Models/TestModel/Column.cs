// <copyright file="Column.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Column
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColumnType { get; set; }

        public Directory BaseClass { get; set; }
    }
}
