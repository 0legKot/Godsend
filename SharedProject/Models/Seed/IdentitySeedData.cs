// <copyright file="IdentitySeedData.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public static class IdentitySeedData
    {
        /// <summary>
        /// Creds for main admin
        /// </summary>
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        private static string[] defaultRoles = new string[] { "Administrator", "Moderator", "Supplier" };

        public static void EnsurePopulated(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            User user = userManager.FindByNameAsync(adminUser).GetAwaiter().GetResult();
            if (!roleManager.Roles.Any())
            {
                foreach (var role in defaultRoles)
                {
                    roleManager.CreateAsync(new Role(role)).GetAwaiter().GetResult();
                }
            }

            if (user == null)
            {
                user = new User(adminUser);
                userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }
        }
    }
}
