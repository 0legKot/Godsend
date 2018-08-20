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
        public static void EnsurePopulated(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            User user = userManager.FindByNameAsync(adminUser).GetAwaiter().GetResult();
            
            Role role = roleManager.FindByNameAsync("Administrator").GetAwaiter().GetResult();
            if (role == null)
            {
                role = new Role("Administrator");
                roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }
            if (user == null)
            {
                
                user = new User("Admin");
                userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }

        }
    }
}
