using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class LinkRatingEntity
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }
    }

    public class LinkRatingProduct : LinkRatingEntity
    {
        public virtual Product Product { get; set; }

        public Guid ProductId { get; set; }
    }

    public class LinkRatingSupplier : LinkRatingEntity
    {
        public virtual Supplier Supplier { get; set; }

        public Guid SupplierId { get; set; }
    }

    public class LinkRatingArticle : LinkRatingEntity
    {
        public virtual Article Article { get; set; }

        public Guid ArticleId { get; set; }
    }
}
