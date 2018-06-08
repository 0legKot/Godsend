namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]

    // Now different because of splitting to Article and ArticleInformation
    public class ArticleController : EntityController<Article>
    {
        //private IArticleRepository repository;

        public ArticleController(IArticleRepository repository)
        {
            this.repository = repository;
        }

        /*[HttpGet("[action]")]
        public IEnumerable<ArticleInformation> All()
        {
            return repository.Entities.Select(a => a.Info);
        }*/

        [HttpGet("[action]/{infoId:Guid}")]
        public Article Detail(Guid infoId)
        {
            var article = repository.Entities.FirstOrDefault(x => x.Info.Id == infoId);
            repository.Watch(article);
            return article;
        }
    }
}
