// <copyright file="Article.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Article
    /// </summary>
    public class Article : IEntity
    {
        public Article()
        { }
        [JsonConstructor]
        public Article(ArticleInformation Info)
        {
            this.Info = new ArticleInformation();
            (this.Info as ArticleInformation).Name = Info.Name;
            (this.Info as ArticleInformation).Description = Info.Description;
            (this.Info as ArticleInformation).Tags = Info.Tags;
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

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        //public ArticleInformation Info { get; set; }

        /// <summary>
        /// Gets or sets the entity information.
        /// </summary>
        /// <value>
        /// The entity information.
        /// </value>
       
        public Information Info { get; set; }

       
    }

}
