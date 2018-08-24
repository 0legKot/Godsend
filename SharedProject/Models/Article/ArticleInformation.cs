// <copyright file="ArticleInformation.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;

    /// <summary>
    /// Article information
    /// </summary>
    public class ArticleInformation : Information
    {
        /// <summary>
        /// Gets or sets the ef author.
        /// </summary>
        /// <value>
        /// The ef author.
        /// </value>
        [JsonIgnore]
        public virtual User EFAuthor { get; set; }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        [NotMapped]
        public virtual ClientUser Author => ClientUser.FromEFUser(EFAuthor);

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// Description that must be less than 100 chars (no ensurenSe)
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ef tags.
        /// </summary>
        /// <value>
        /// The ef tags.
        /// </value>
        [JsonIgnore]
        public virtual IEnumerable<StringWrapper> EFTags { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [NotMapped]
        public virtual IEnumerable<string> Tags
        {
            get => this.EFTags?.Select(x => x.Value)?.ToArray();
            set => this.EFTags = value?.Select<string, StringWrapper>(s => s).ToArray();
        }

        [JsonIgnore]
        public virtual Article Article { get; set; }
    }
}
