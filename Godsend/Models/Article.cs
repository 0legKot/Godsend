namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Article
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public ArticleInformation Info { get; set; }
    }

    public class SimpleArticle : Article
    {
    }
}
