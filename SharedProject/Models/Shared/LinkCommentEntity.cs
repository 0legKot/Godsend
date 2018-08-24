// <copyright file="LinkCommentEntity.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
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

        [JsonIgnore]
        public virtual LinkCommentEntity BaseComment { get; set; }

        /// <summary>
        /// Warning: not set by EF Core
        /// </summary>
        public Guid? BaseCommentId { private get; set; }

        [NotMapped]
        public virtual ClientUser Author => ClientUser.FromEFUser(User);

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

    public class Comment
    {
        public string CommentText { get; set; }

        public Comment BaseComment { get; set; }

        public User User { get; set; }
    }

    public class CommentWithSubs
    {
        public LinkCommentEntity Comment { get; set; }

        public List<CommentWithSubs> Subs { get; set; }
    }
}
