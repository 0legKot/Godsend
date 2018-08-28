// <copyright file="EFArticleRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///
    /// </summary>
    public class EFArticleRepository : IArticleRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        /// <summary>
        /// The user
        /// </summary>
        private User user;

        private IRatingHelper ratingHelper;

        private ICommentHelper commentHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFArticleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userManager">The user manager.</param>
        public EFArticleRepository(DataContext context, ISeedHelper seedHelper, IRatingHelper ratingHelper, ICommentHelper commentHelper)
        {
            this.context = context;
            this.ratingHelper = ratingHelper;
            this.commentHelper = commentHelper;
            seedHelper.EnsurePopulated(context);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<Article> Entities(int quantity, int skip = 0)
        {
            var tmp = context.Articles.Skip(skip).Take(quantity).ToArray();
            return tmp;
        }

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <value>
        /// The entities information.
        /// </value>
        public IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0)
        {
            var tmp = context.Articles.Select(a => a.Info).Skip(skip).Take(quantity);

            return tmp;
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="id">The article identifier.</param>
        public async Task DeleteEntity(Guid id)
        {
            Article dbEntry = GetEntity(id);
            if (dbEntry != null)
            {
                var quantumMagic = dbEntry.Info.EFTags;

                ////context.RemoveRange(context.LinkRatingArticle.Where(lra => lra.EntityId == dbEntry.Id));
                ////context.RemoveRange(context.LinkCommentArticle.Where(lra => lra.ArticleId == dbEntry.Id));
                context.Articles.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Watches the specified art.
        /// </summary>
        /// <param name="art">The art.</param>
        public void Watch(Article art)
        {
            if (art != null)
            {
                ++art.Info.Watches;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        public Article GetEntity(Guid entityId)
        {
            return context.Articles.FirstOrDefault(a => a.Id == entityId);
        }

        /// <summary>
        /// Determines whether the specified entity is first.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity is first; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFirst(Article entity)
        {
            return !context.Articles.Any(a => a.Id == entity.Id);
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task SaveEntity(Article entity)
        {
            Article dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                entity.CopyTo(dbEntry);
            }
            else
            {
                entity.Info.Created = DateTime.Now;

                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        public int EntitiesCount()
        {
            return context.Articles.Count();
        }

        public async Task<double> SetRatingAsync(Guid articleId, string userId, int rating)
        {
            await ratingHelper.SetRatingAsync(articleId, userId, rating, context.LinkRatingArticle, context);

            return await RecalcRatings(articleId);
        }

        private async Task<double> RecalcRatings(Guid articleId)
        {
            var avg = await ratingHelper.CalculateAverageAsync(context.LinkRatingArticle, articleId);

            var article = await context.Articles.FirstOrDefaultAsync(p => p.Id == articleId);
            article.Info.Rating = avg;

            await context.SaveChangesAsync();

            return avg;
        }

        public IEnumerable<LinkRatingEntity> GetAllRatings(Guid articleId)
        {
            return context.LinkRatingArticle.Where(lra => lra.EntityId == articleId);
        }

        public int? GetUserRating(Guid articleId, string userId)
        {
            return context.LinkRatingArticle.FirstOrDefault(lra => lra.EntityId == articleId && lra.UserId == userId)?.Rating;
        }

        public async Task<Guid> AddCommentAsync(Guid articleId, string userId, Guid? baseCommentId, string comment)
        {
            var newCommentId = await commentHelper.AddCommentGenericAsync<LinkCommentArticle>(context, articleId, userId, baseCommentId, comment);

            await RecalcCommentsAsync(articleId); // or just increment?

            return newCommentId;
        }

        public IEnumerable<LinkCommentEntity> GetAllComments(Guid articleId)
        {
            var fortst = context.LinkCommentArticle.Where(lra => lra.Article.Id == articleId)
                .Select(x => new LinkCommentArticle() { BaseComment = x.BaseComment, Comment = x.Comment, Id = x.Id, User = x.User });
            return fortst;
        }

        public async Task DeleteCommentAsync(Guid articleId, Guid commentId, string userId)
        {
            await commentHelper.DeleteCommentGenericAsync(context.LinkCommentArticle, context, articleId, commentId);

            await RecalcCommentsAsync(articleId);
        }

        public async Task EditCommentAsync(Guid commentId, string newContent, string userId)
        {
            var comment = await context.LinkCommentArticle.FirstOrDefaultAsync(lce => lce.Id == commentId);
            if (comment.UserId != userId) throw new InvalidOperationException("Incorrect user tried to edit comment");
            comment.Comment = newContent;

            await context.SaveChangesAsync();
        }

        public async Task RecalcCommentsAsync(Guid articleId)
        {
            var commentCount = await context.LinkCommentArticle.CountAsync(lce => lce.ArticleId == articleId);

            var article = await context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);

            article.Info.CommentsCount = commentCount;

            await context.SaveChangesAsync();
        }
    }
}
