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
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// Article controller
    /// </summary>
    /// <seealso cref="Controllers.EntityController{Article}" />
    [Route("api/[controller]")]
    public class ArticleController : EntityController<Article>
    {
        IHubContext<NotificationController> hubContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ArticleController(IArticleRepository repository, IHubContext<NotificationController> hubContext)
        {
            this.repository = repository;
            this.hubContext = hubContext;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<IActionResult> CreateOrUpdate([FromBody] Article entity)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "sub");

            var repo = repository as IArticleRepository;
            await repo.SetUserAsync(name.Value);

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

            await hubContext.Clients.All.SendAsync("Send","hahaha");

            return article;
        }
    }
}
