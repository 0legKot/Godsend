// <copyright file="Category.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Category
    {
        public Guid Id { get; set; }

        public Category BaseCategory { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return "Category: " + Name;
        }
    }
}
