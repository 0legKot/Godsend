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
        /// The user manager
        /// </summary>
        private UserManager<User> userManager;

        /// <summary>
        /// The user
        /// </summary>
        private User user;

        private IRatingHelper ratingHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFArticleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userManager">The user manager.</param>
        public EFArticleRepository(DataContext context, UserManager<User> userManager, ISeedHelper seedHelper, IRatingHelper ratingHelper)
        {
            this.context = context;
            this.userManager = userManager;
            this.ratingHelper = ratingHelper;
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
            var tmp = context.Articles.Include(a => (a.Info as ArticleInformation)).ThenInclude(ai => ai.EFAuthor).Skip(skip).Take(quantity).ToArray();
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
            var tmp = context.Articles.Include(a=>a.Info).ThenInclude(a=>(a as ArticleInformation).EFTags).ThenInclude(a => (a as ArticleInformation).EFAuthor).Select(a=>a.Info)
                                                          .Skip(skip).Take(quantity);

            return tmp;
        }

        /// <summary>
        /// Sets the user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task SetUserAsync(string email)
        {
            user = await userManager.FindByNameAsync(email);
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        public async Task DeleteEntity(Guid infoId)
        {
            Article dbEntry = GetEntityByInfoId(infoId);
            if (dbEntry != null)
            {
                context.RemoveRange(context.LinkRatingArticle.Where(lra => lra.ArticleId == dbEntry.Id));
                context.RemoveRange(context.LinkCommentArticle.Where(lra => lra.ArticleId == dbEntry.Id));
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
            return context.Articles
            //    .Include(a => (a.Info as ArticleInformation)).ThenInclude(a => a.EFAuthor)
            //.Include(a => (a.Info as ArticleInformation)).ThenInclude(a => a.EFTags)
            .FirstOrDefault(a => a.Id == entityId);
        }

        /// <summary>
        /// Gets the entity by information identifier.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        /// <returns></returns>
        //TODO: fix includes
        public Article GetEntityByInfoId(Guid infoId)
        {
            return context.Articles.Include(a => a.Info)
                //.ThenInclude(a => ((ArticleInformation)a).EFAuthor)
                //.Include(a => a.Info).ThenInclude(a => ((ArticleInformation)a).EFTags)
            .FirstOrDefault(a => a.Info.Id == infoId);
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
        /// <exception cref="Exception">Not authorized</exception>
        public async Task SaveEntity(Article entity)
        {
            //if (user == null)
            //{
            //    throw new Exception("Not authorized");
            //}

            Article dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                (dbEntry.Info as ArticleInformation).Name = entity.Info.Name;
                (dbEntry.Info as ArticleInformation).Description = (entity.Info as ArticleInformation).Description;
                dbEntry.Content = entity.Content;

                // ....
            }
            else
            {
                ////entity.SetIds();
                (entity.Info as ArticleInformation).EFAuthor = user;
                (entity.Info as ArticleInformation).Created = DateTime.Now;

                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        public int EntitiesCount()
        {
            return context.Articles.Count();
        }

        public async Task<double> SetRating(Guid articleId, string userId, int rating)
        {
            await ratingHelper.SetRating(articleId, userId, rating, context.LinkRatingArticle, lra => lra.ArticleId, context);

            return await RecalcRatings(articleId);
        }

        private async Task<double> RecalcRatings(Guid articleId)
        {
            var avg = await ratingHelper.CalculateAverage(context.LinkRatingArticle, lra => lra.ArticleId, articleId);

            await ratingHelper.SaveAverage(context.Articles, articleId, avg, context);

            return avg;
        }

        public IEnumerable<LinkRatingEntity> GetAllRatings(Guid articleId)
        {
            return context.LinkRatingArticle.Where(lra => lra.ArticleId == articleId);
        }

        public int? GetUserRating(Guid articleId, string userId)
        {
            return context.LinkRatingArticle.FirstOrDefault(lra => lra.ArticleId == articleId && lra.UserId == userId)?.Rating;
        }

        public async Task<Guid> AddCommentAsync(Guid articleId, string userId, Guid baseCommentId, string comment)
        {
            var newComment = new LinkCommentArticle { ArticleId = articleId, UserId = userId,
                BaseComment = new LinkCommentArticle() { Id = baseCommentId },
                Comment = comment
            };
            context.Add(newComment);
            await context.SaveChangesAsync();
            return newComment.Id;
        }

        public IEnumerable<LinkCommentEntity> GetAllComments(Guid articleId)
        {
            var fortst = context.LinkCommentArticle.Where(lra => lra.ArticleId == articleId)
                .Select(x=>new LinkCommentEntity() { BaseComment=x.BaseComment, Comment=x.Comment,Id=x.Id, User=x.User } );
            return fortst;
        }
        public class Comment
        {
            public string CommentText { get; set; }
            public Comment BaseComment { get; set; }
            public User User { get; set; }
        }
        public class CommentWithSubs
        {
            public LinkCommentEntity Comment { get; set; }
            public List<CommentWithSubs> Subs { get; set; }
        }
    }
}
