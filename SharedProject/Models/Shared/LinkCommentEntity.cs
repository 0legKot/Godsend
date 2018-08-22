﻿namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using Newtonsoft.Json;

    public class LinkCommentEntity
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public virtual LinkCommentEntity BaseComment { get; set; }

        [NotMapped]
        public virtual ClientUser Author => User == null ? null : new ClientUser { Id = User.Id, Name = User.UserName };

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }

    public class LinkCommentProduct : LinkCommentEntity
    {
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }
    }

    public class LinkCommentSupplier : LinkCommentEntity
    {
        public virtual Supplier Supplier { get; set; }

        public Guid SupplierId { get; set; }
    }

    public class LinkCommentArticle : LinkCommentEntity
    {
        public virtual Article Article { get; set; }

        public Guid ArticleId { get; set; }
    }
}
