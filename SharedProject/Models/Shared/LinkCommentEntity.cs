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

    public class LinkCommentEntity<T> where T:IEntity
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public virtual T Entity { get; set; }

        [JsonIgnore]
        public virtual LinkCommentEntity<T> BaseComment { get; set; }

        public Guid? BaseCommentId { get; set; }

        [NotMapped]
        public virtual ClientUser Author => ClientUser.FromEFUser(User);

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<LinkCommentEntity<T>> ChildComments { get; set; }

        [JsonIgnore]
        [NotMapped]
        public Guid EntityId { get; set; }
    }


    public class Comment
    {
        public string CommentText { get; set; }

        public Comment BaseComment { get; set; }

        public User User { get; set; }
    }

    public class CommentWithSubs<T> where T:IEntity
    {
        public LinkCommentEntity<T> Comment { get; set; }

        public List<CommentWithSubs<T>> Subs { get; set; }
    }
}
