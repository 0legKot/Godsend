// <copyright file="Article.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Article
    /// </summary>
    public class Article : IEntity
    {
        public Article()
        {
        }

        [JsonConstructor]
        public Article(ArticleInformation Info)
        {
            this.Info = new ArticleInformation()
            {
                Name = Info.Name,
                Description = Info.Description,
                Tags = Info.Tags
            };
        }

        public Article(string content, User author, string description, string name, string[] tags, double rating, int watches)
        {
            this.Content = content;
            this.Info = new ArticleInformation()
            {
                EFAuthor = author,
                Description = description,
                Created = DateTime.Now,
                Name = name,
                Tags = tags,
                Rating = rating,
                Watches = watches,
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        public virtual ArticleInformation Info { get; set; }
    }
}
