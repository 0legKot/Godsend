// <copyright file="StringWrapper.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StringWrapper
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public static implicit operator StringWrapper(string s)
        {
            return new StringWrapper() { Value = s };
        }
    }
}
