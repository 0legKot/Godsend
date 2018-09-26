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
    using Microsoft.Extensions.Logging;

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
        protected Repository<TEntity> repository;
        protected IHubContext<NotificationHub> hubContext;
        protected readonly ILogger<EntityController<TEntity>> _logger;

        protected EntityController(IHubContext<NotificationHub> hubContext, ILogger<EntityController<TEntity>> logger)
        {
            this.hubContext = hubContext;
            _logger = logger;
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
            _logger.LogInformation($"Executed EntityController<{typeof(TEntity)}>");
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
        [Authorize(Roles = "Administrator,Moderator")]
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
        [Authorize(Roles = "Administrator,Moderator")]
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

                return Ok(entity.Id);
            }
            catch (Exception ex)
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
        public virtual IEnumerable<LinkRatingEntity<TEntity>.WithoutEntity> Ratings(Guid entityId)
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
        [HttpPost("[action]/{entityId:Guid}/{baseCommentId:Guid}")]
        public virtual async Task<IActionResult> AddComment(Guid entityId, Guid? baseCommentId, [FromBody]TmpComment comment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                var newCommentId = await repository.AddCommentAsync(entityId, userId, baseCommentId, comment.Comment);

                await hubContext.Clients.User(userId).SendAsync("Success", "Comment has been added");

                return Ok(newCommentId);
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not add a comment");

                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("[action]/{entityId:Guid}")]
        public virtual async Task<IActionResult> AddComment(Guid entityId, [FromBody]TmpComment comment)
        {
            return await AddComment(entityId, null, comment);

        }

        [Authorize]
        [HttpDelete("[action]/{entityId:Guid}/{commentId:Guid}")]
        public virtual async Task<IActionResult> DeleteOwnComment(Guid entityId, Guid commentId)
        {
            throw new NotImplementedException();
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                await repository.DeleteCommentAsync(entityId, commentId, userId);

                await hubContext.Clients.User(userId).SendAsync("Success", "Comment has been deleted");

                return Ok();
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not delete a comment");

                return BadRequest();
            }
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpDelete("[action]/{entityId:Guid}/{commentId:Guid}")]
        public virtual async Task<IActionResult> DeleteComment(Guid entityId, Guid commentId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                await repository.DeleteCommentAsync(entityId, commentId, userId);

                await hubContext.Clients.User(userId).SendAsync("Success", "Comment has been deleted");

                return Ok();
            }
            catch (Exception ex)
            {
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not delete a comment");

                return BadRequest();
            }
        }

        [HttpPatch("[action]/{commentId:Guid}")]
        [Authorize]
        public virtual async Task<IActionResult> EditOwnComment(Guid commentId, [FromBody]TmpComment comment)
        {
            throw new NotImplementedException();
            //TODO: rework
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                await repository.EditCommentAsync(commentId, comment.Comment, userId);

                await hubContext.Clients.User(userId).SendAsync("Success", "Comment has been edited");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not edit a comment");

                return BadRequest();
            }
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpPatch("[action]/{commentId:Guid}")]
        public virtual async Task<IActionResult> EditComment(Guid commentId, [FromBody]TmpComment comment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                await repository.EditCommentAsync(commentId, comment.Comment, userId);

                await hubContext.Clients.User(userId).SendAsync("Success", "Comment has been edited");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await hubContext.Clients.User(userId).SendAsync("Error", "Could not edit a comment");

                return BadRequest();
            }
        }

        [HttpGet("[action]/{entityId:Guid}")]
        public virtual IEnumerable<CommentWithSubs> Comments(Guid entityId)
        {
            CommentsArr = repository.GetAllComments(entityId);

            if (!CommentsArr.Any())
            {
                return null;
            }

            var baseComments = CommentsArr.Where(lce => lce.BaseComment == null)
                .Select(lce => new CommentWithSubs()
                {
                    Comment = lce,
                    Subs = new List<CommentWithSubs>()
                }).ToArray();

            foreach (var comment in baseComments)
            {
                GetRecursiveComs(comment);
            }
            return baseComments;
            /*CommentWithSubs tmplst = new CommentWithSubs()
            {
                Comment = CommentsArr.FirstOrDefault(x => x.BaseComment == null),
                Subs = new List<CommentWithSubs>()
            };*/
            //GetRecursiveComs(ref tmplst);

        }

        public IEnumerable<LinkCommentEntity> GetSubComments(Guid id)
        {
            return CommentsArr.Where(x => x.BaseComment?.Id == id);
        }

        private void GetRecursiveComs(CommentWithSubs cur)
        {
            var subs = new List<CommentWithSubs>();
            var curSubComs = GetSubComments(cur.Comment.Id);
            if (curSubComs.Any())
            {
                foreach (var com in curSubComs)
                {
                    var tmp = new CommentWithSubs() { Comment = com };
                    GetRecursiveComs(tmp);
                    //var tmpClone = new CommentWithSubs() { Comment = tmp.Comment, Subs = tmp.Subs };
                    //tmpClone.Comment.BaseComment = null;
                    subs.Add(tmp);
                }
            }

            cur.Subs = subs;
        }
    }

    public class TmpComment
    {
        public string Comment { get; set; }
    }
}
