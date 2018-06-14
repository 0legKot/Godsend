// <copyright file="ArticleController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ArticleController : EntityController<Article>
    {
        public ArticleController(IArticleRepository repository)
        {
            this.repository = repository;
        }

        /*[HttpGet("[action]")]
        public IEnumerable<ArticleInformation> All()
        {
            return repository.Entities.Select(a => a.Info);
        }*/

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override IActionResult CreateOrUpdate([FromBody] Article entity)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "sub");
            (repository as IArticleRepository).SetUser(email.Value);

            return base.CreateOrUpdate(entity);
        }

        [HttpGet("[action]/{infoId:Guid}")]
        public Article Detail(Guid infoId)
        {
            var article = repository.Entities.FirstOrDefault(x => x.Info.Id == infoId);
            repository.Watch(article);
            return article;
        }
    }
}
