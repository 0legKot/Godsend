// <copyright file="Information.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Information
    {
         public Guid Id { get; set; }

         public string Name { get; set; }

         public double Rating { get; set; }

         public int Watches { get; set; }
    }
}
