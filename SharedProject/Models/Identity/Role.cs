using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Godsend.Models
{
    public class Role:IdentityRole<string>
    {
        public Role() : base()
        {
        }
        public Role(string name) : base(name)
        {
        }
    }
}
