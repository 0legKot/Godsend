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
    using Microsoft.Extensions.Logging;

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
        public ArticleController(AArticleRepository repository, IHubContext<NotificationHub> hubContext, ILogger<EntityController<Article>> logger)
            : base(hubContext, logger)
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
        [Authorize(Roles ="Administrator,Moderator")]
        public override async Task<IActionResult> CreateOrUpdate([FromBody] Article entity)
        {
            var nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (entity.Id == Guid.Empty)
            {
                entity.Info.EFAuthorId = nameId;
            }

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
            var article = repository.GetEntity(infoId);
            repository.Watch(article);

            await hubContext.Clients.All.SendAsync("Success", "Somebody has just watched the article!");

            return article;
        }
    }
}
