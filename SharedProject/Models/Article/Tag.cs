using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public virtual IEnumerable<LinkArticleTag> LinkArticleTags { get; set; }

        public Tag(string value)
        {
            Value = value;
        }
    }

    public class LinkArticleTag
    {
        public Guid Id { get; set; }

        public virtual Article Article { get; set; }

        public Guid ArticleId { get; set; }

        public virtual Tag Tag { get; set; }

        public Guid TagId { get; set; }

        public class WithoutArticle
        {
            public Guid Id { get; set; }

            public Tag Tag { get; set; }

            public WithoutArticle(LinkArticleTag link)
            {
                if (link != null)
                {
                    Id = link.Id;
                    Tag = link.Tag;
                }
            }
        }
    }
}
