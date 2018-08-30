using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Godsend.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Required]
        public string Value { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<LinkArticleTag> LinkArticleTags { get; set; }

        public Tag(string value)
        {
            Value = value;
        }
    }

    public class LinkArticleTag
    {
        public Guid Id { get; set; }

        public virtual ArticleInformation ArticleInfo { get; set; }

        public Guid ArticleInfoId { get; set; }

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
