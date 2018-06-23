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
        private UserManager<IdentityUser> userManager;

        /// <summary>
        /// The user
        /// </summary>
        private IdentityUser user = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFArticleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userManager">The user manager.</param>
        public EFArticleRepository(DataContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;

            SeedHelper.EnsurePopulated(context);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<Article> Entities(int quantity, int skip = 0)
        {
            var tmp = context.Articles.Include(a => a.Info).ThenInclude(ai => ai.EFAuthor).Take(quantity).Skip(skip).ToArray();
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
            var tmp = context.Articles.Select(x => x.Info).Include(ai => ai.EFTags).Include(ai => ai.EFAuthor).Skip(skip).Take(quantity);

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
        public void DeleteEntity(Guid infoId)
        {
            Article dbEntry = GetEntityByInfoId(infoId);
            if (dbEntry != null)
            {
                context.Articles.Remove(dbEntry);
                context.SaveChanges();
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
            return context.Articles.Include(a => a.Info).ThenInclude(a => a.EFAuthor)
            .Include(a => a.Info).ThenInclude(a => a.EFTags).FirstOrDefault(a => a.Id == entityId);
        }

        /// <summary>
        /// Gets the entity by information identifier.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        /// <returns></returns>
        public Article GetEntityByInfoId(Guid infoId)
        {
            return context.Articles.Include(a => a.Info).ThenInclude(a => a.EFAuthor)
            .Include(a => a.Info).ThenInclude(a => a.EFTags).FirstOrDefault(a => a.Info.Id == infoId);
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
        public void SaveEntity(Article entity)
        {
            if (user == null)
            {
                throw new Exception("Not authorized");
            }

            Article dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = entity.Info.Name;
                dbEntry.Content = entity.Content;

                // dbEntry.Status = supplier.Status;
                // ....
            }
            else
            {
                ////entity.SetIds();
                entity.Info.EFAuthor = user;
                entity.Info.Created = DateTime.Now;

                context.Add(entity);
            }

            context.SaveChanges();
        }
    }
}
