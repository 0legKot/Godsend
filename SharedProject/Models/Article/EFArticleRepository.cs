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

        /// <summary>
        /// Initializes a new instance of the <see cref="EFArticleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userManager">The user manager.</param>
        public EFArticleRepository(DataContext context, UserManager<User> userManager, ISeedHelper seedHelper)
        {
            this.context = context;
            this.userManager = userManager;

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
            var user = await userManager.FindByIdAsync(userId);

            var existingRating = await context.LinkRatingArticle.FirstOrDefaultAsync(lra => lra.UserId == userId && lra.ArticleId == articleId);

            if (existingRating == null)
            {
                var newRating = new LinkRatingArticle { ArticleId = articleId, UserId = userId, Rating = rating };
                context.Add(newRating);
            }
            else
            {
                existingRating.Rating = rating;
            }

            await context.SaveChangesAsync();

            return await RecalcRatings(articleId);
        }

        private async Task<double> RecalcRatings(Guid articleId)
        {
            var ratingsExist = await context.LinkRatingArticle.AnyAsync(lra => lra.ArticleId == articleId);

            var avg = ratingsExist
                ? await context.LinkRatingArticle
                    .Where(lra => lra.ArticleId == articleId)
                    .Select(lra => lra.Rating)
                    .AverageAsync()
                : 0;

            var article = await context.Articles.Include(a => a.Info).FirstOrDefaultAsync(a => a.Id == articleId);
            article.Info.Rating = avg;

            await context.SaveChangesAsync();

            return avg;
        }
    }
}
