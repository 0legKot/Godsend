namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        IEnumerable<Article> Articles { get; }

        void SaveArticle(Article article);

        void DeleteArticle(Guid articleId);
    }
}
