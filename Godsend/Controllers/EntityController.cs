// <copyright file="EntityController.cs" company="Godsend Team">
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
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// Base controller for entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public abstract class EntityController<TEntity> : Controller
        where TEntity : IEntity
    {
        /// <summary>
        /// The repository for instances
        /// </summary>
        protected IRepository<TEntity> repository;
        protected IHubContext<NotificationHub> hubContext;

        protected EntityController(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        /// <summary>
        /// All instances.
        /// </summary>
        /// <param name="page">Current page.</param>
        /// <param name="rpp">Results per page.</param>
        /// <returns>rpp Instances for current page</returns>
        [HttpGet("[action]/{page:int}/{rpp:int}")]
        public virtual IEnumerable<Information> All(int page, int rpp)
        {
            return repository.EntitiesInfo(rpp, (page - 1) * rpp);
        }

        [HttpGet("[action]")]
        public virtual int Count()
        {
            return repository.EntitiesCount();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id">The identifier of entity that must be deleted.</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id:Guid}")]
        [Authorize]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                await repository.DeleteEntity(id);
                await hubContext.Clients.User(userId).SendAsync("Success", "Deleted successfully");
                return Ok();
            }
            catch (Exception e)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not delete entity");
                return BadRequest();
            }
        }

        /// <summary>
        /// Creates or updates specified entity asynchronous.
        /// Creates if entity didn't exist
        /// </summary>
        /// <param name="entity">Entity for updating or creating.</param>
        /// <returns>Ok on success, BadRequest else </returns>
        [HttpPost("[action]")]
        [Authorize]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody]TEntity entity)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var creating = entity.Id == Guid.Empty;

            try
            {
                await repository.SaveEntity(entity);

                await (creating
                    ? hubContext.Clients.User(userId).SendAsync("Success", "Created successfully")
                    : hubContext.Clients.User(userId).SendAsync("Success", "Saved successfully"));

                return Ok(entity.Info.Id);
            }
            catch
            {
                await (creating
                    ? hubContext.Clients.User(userId).SendAsync("Error", "Could not create")
                    : hubContext.Clients.User(userId).SendAsync("Error", "Could not save"));

                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("[action]/{entityId:Guid}/{rating:int}")]
        public virtual async Task<IActionResult> SetRating(Guid entityId, int rating)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                var avg = await repository.SetRatingAsync(entityId, userId, rating);

                await hubContext.Clients.User(userId).SendAsync("Success", "Rating has been saved");

                return Ok(avg);
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not save rating");

                return BadRequest();
            }
        }

        [HttpGet("[action]/{entityId:Guid}")]
        public virtual IEnumerable<LinkRatingEntity.WithoutEntity> Ratings(Guid entityId)
        {
            return repository.GetAllRatings(entityId).Select(link => link.GetWithoutEntity());
        }

        [Authorize]
        [HttpGet("[action]/{entityId:Guid}")]
        public virtual int? Rating(Guid entityId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return repository.GetUserRating(entityId, userId);
        }

        IEnumerable<LinkCommentEntity> CommentsArr;

        [Authorize]
        [HttpPost("[action]/{entityId:Guid}/{baseCommentId:Guid}/{comment}")]
        public virtual async Task<IActionResult> AddComment(Guid entityId, Guid baseCommentId, string comment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                var newCommentId = await repository.AddCommentAsync(entityId, userId, baseCommentId, comment);

                await hubContext.Clients.User(userId).SendAsync("Success", "Comment has been added");

                return Ok(newCommentId);
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not add a comment");

                return BadRequest();
            }
        }

        [HttpGet("[action]/{entityId:Guid}")]
        public virtual CommentWithSubs Comments(Guid entityId)
        {
            CommentsArr = repository.GetAllComments(entityId);
            CommentWithSubs tmplst = new CommentWithSubs()
            {
                Comment = CommentsArr.FirstOrDefault(x => x.BaseComment == null),
                Subs = new List<CommentWithSubs>()
            };
            GetRecursiveComs(ref tmplst);
            return tmplst;
        }

        public IEnumerable<LinkCommentEntity> GetSubComments(Guid id)
        {
            return CommentsArr.Where(x => x.BaseComment?.Id == id);
        }

        private void GetRecursiveComs(ref CommentWithSubs cur)
        {
            var subs = new List<CommentWithSubs>();
            var curSubComs = GetSubComments(cur.Comment.Id);
            if (curSubComs.Any())
            {
                foreach (var com in curSubComs)
                {
                    var tmp = new CommentWithSubs() { Comment = com };
                    GetRecursiveComs(ref tmp);
                    var tmpClone = new CommentWithSubs() { Comment = tmp.Comment, Subs = tmp.Subs };
                    tmpClone.Comment.BaseComment = null;
                    subs.Add(tmpClone);
                }
            }

            cur.Subs = subs;
        }

        /////// <summary>
        /////// Edits the entity asynchronous.
        /////// </summary>
        /////// <param name="entity">The entity.</param>
        /////// <returns></returns>
        ////[HttpPatch("[action]/{id:Guid}")]
        ////public virtual async Task<IActionResult> EditAsync([FromBody]TEntity entity)
        ////{
        ////    return await CreateOrUpdate(entity);
        ////}

        /////// <summary>
        /////// Creates the entity asynchronous.
        /////// </summary>
        /////// <param name="entity">The entity.</param>
        /////// <returns></returns>
        ////[HttpPut("[action]/{id:Guid}")]
        ////public virtual async Task<IActionResult> CreateAsync([FromBody]TEntity entity)
        ////{
        ////    return await CreateOrUpdate(entity);
        ////}

        /*[HttpGet("[action]/{id:Guid}")]
        public virtual TEntity Detail(Guid id)
        {
            return repository.GetEntity(id);
        }*/
    }
}
