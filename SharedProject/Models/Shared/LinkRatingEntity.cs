using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Godsend.Models
{
    public class LinkRatingEntity
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }

        [NotMapped]
        public virtual ClientUser Author => User == null ? null : new ClientUser { Id = User.Id, Name = User.UserName };

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        public virtual void SetEntityId(Guid entityId)
        {
            throw new NotSupportedException();
        }
    }

    public class LinkRatingProduct : LinkRatingEntity
    {
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }

        public override void SetEntityId(Guid entityId)
        {
            ProductId = entityId;
        }
    }

    public class LinkRatingSupplier : LinkRatingEntity
    {
        public virtual Supplier Supplier { get; set; }

        public Guid SupplierId { get; set; }

        public override void SetEntityId(Guid entityId)
        {
            SupplierId = entityId;
        }
    }

    public class LinkRatingArticle : LinkRatingEntity
    {
        public virtual Article Article { get; set; }

        public Guid ArticleId { get; set; }

        public override void SetEntityId(Guid entityId)
        {
            ArticleId = entityId;
        }
    }
}
