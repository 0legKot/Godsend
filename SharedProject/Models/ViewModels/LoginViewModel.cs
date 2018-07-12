// <copyright file="LoginViewModel.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LoginViewModel
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
