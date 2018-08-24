// <copyright file="LinkRatingEntity.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using Newtonsoft.Json;

    public class LinkRatingEntity
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }

        [NotMapped]
        public virtual ClientUser Author => ClientUser.FromEFUser(User);

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        public Guid EntityId { get; set; }

        /// <summary>
        /// Get instance of the class <see cref="WithoutEntity"/>, which has properties Id, Author and Rating
        /// </summary>
        public WithoutEntity GetWithoutEntity() => new WithoutEntity
        {
            Id = Id,
            Author = Author,
            Rating = Rating
        };

        public class WithoutEntity
        {
            public Guid Id { get; set; }

            public int Rating { get; set; }

            public virtual ClientUser Author { get; set; }
        }
    }

    public class LinkRatingProduct : LinkRatingEntity
    {
        public virtual Product Entity { get; set; }
    }

    public class LinkRatingSupplier : LinkRatingEntity
    {
        public virtual Supplier Entity { get; set; }
    }

    public class LinkRatingArticle : LinkRatingEntity
    {
        public virtual Article Entity { get; set; }
    }
}
