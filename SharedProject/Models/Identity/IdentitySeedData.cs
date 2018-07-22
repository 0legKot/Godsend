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

    /// <summary>
    ///
    /// </summary>
    public static class IdentitySeedData
    {
        /// <summary>
        /// The admin user
        /// </summary>
        private const string adminUser = "Admin";

        /// <summary>
        /// The admin password
        /// </summary>
        private const string adminPassword = "Secret123$";

        /// <summary>
        /// Ensures the populated.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <returns></returns>
        public static async Task EnsurePopulated(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            User user = await userManager.FindByNameAsync(adminUser);
            
            Role role = await roleManager.FindByNameAsync("Administrator");
            if (role == null)
            {
                role = new Role("Administrator");
                await roleManager.CreateAsync(role);
            }
            if (user == null)
            {
                
                user = new User("Admin");
                userManager.AddToRoleAsync(user, "Administrator");
                await userManager.CreateAsync(user, adminPassword);
            }

        }
    }
}
