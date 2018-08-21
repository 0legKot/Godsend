// <copyright file="ArticleController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// Article controller
    /// </summary>
    /// <seealso cref="Controllers.EntityController{Article}" />
    [Route("api/[controller]")]
    public class ArticleController : EntityController<Article>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ArticleController(IArticleRepository repository, IHubContext<NotificationHub> hubContext) 
            : base(hubContext)
        {
            this.repository = repository;
        }

        /*[HttpGet("[action]")]
        public IEnumerable<ArticleInformation> All()
        {
            return repository.Entities.Select(a => a.Info);
        }*/

        /// <summary>
        /// Creates the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [Authorize]
        public override async Task<IActionResult> CreateOrUpdate([FromBody] Article entity)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var repo = repository as IArticleRepository;
            await repo.SetUserAsync(name);

            return await base.CreateOrUpdate(entity);
        }

        /// <summary>
        /// Details the specified information identifier.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        /// <returns></returns>
        [HttpGet("[action]/{infoId:Guid}")]
        public async Task<Article> Detail(Guid infoId)
        {
            var article = repository.GetEntityByInfoId(infoId);
            repository.Watch(article);

            await hubContext.Clients.All.SendAsync("Success","Somebody has just watched the article!");

            //await hubContext.Clients.User(null).SendAsync("a", "aaa");
            return article;
        }
    }
}
