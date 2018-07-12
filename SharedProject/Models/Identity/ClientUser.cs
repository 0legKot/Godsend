// <copyright file="ClientUser.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public class ClientUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; } // remove?

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the birth.
        /// </summary>
        /// <value>
        /// The birth.
        /// </value>
        public DateTime? Birth { get; set; }

        /// <summary>
        /// Gets or sets the registration date.
        /// </summary>
        /// <value>
        /// The registration date.
        /// </value>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Rating { get; set; }

        public static ClientUser FromEFUser(User user)
        {
            return new ClientUser
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Birth = user.Birth,
                Rating = user.Rating,
                RegistrationDate = user.RegistrationDate
            };
        }

        public static ClientUser FromEFUserGeneralInfo(User user)
        {
            return new ClientUser
            {
                Name = user.UserName,
                Email = user.Email,
                Birth = null,
                Rating = user.Rating,
                RegistrationDate = user.RegistrationDate
            };
        }

    }
}
