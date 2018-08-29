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
        public Article(ArticleInformation info)
        {
            this.Info = new ArticleInformation()
            {
                Name = info.Name,
                Description = info.Description,
                Tags = info.Tags
            };
        }

        public Article(string content, User author, string description, string name, double rating, int watches)
        {
            this.Content = content;
            this.Info = new ArticleInformation()
            {
                EFAuthor = author,
                Description = description,
                Created = DateTime.Now,
                Name = name,
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

        public void CopyTo(Article target)
        {
            // todo tags
            target.Content = Content;
            target.Info.Description = Info.Description;
            target.Info.Name = Info.Name;
        }
    }
}
