// <copyright file="Role.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole<string>
    {
        public Role() : base()
        {
        }

        public Role(string name) : base(name)
        {
        }
    }
}
