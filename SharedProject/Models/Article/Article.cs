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

    public class Article : IEntity
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public ArticleInformation Info { get; set; }

        [JsonIgnore]
        public Information EntityInformation
        {
            get => Info; set { Info = value as ArticleInformation; }
        }
    }

    public class SimpleArticle : Article
    {
    }
}
