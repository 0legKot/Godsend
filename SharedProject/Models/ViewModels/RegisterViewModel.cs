// <copyright file="RegisterViewModel.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RegisterViewModel
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

        /// <summary>
        /// Gets or sets name
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the birth.
        /// </summary>
        /// <value>
        /// The birth.
        /// </value>
        [Required]
        public string Birth { get; set; }
    }
}
