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
    public class EFArticleRepository : ArticleRepository
    {

        private User user;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFArticleRepository"/> class.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="userManager">The user manager.</param>
        public EFArticleRepository(DataContext ctx, ISeedHelper seedHelper, ICommentHelper<Article> commentHelper)
            : base(ctx, seedHelper,commentHelper)
        {
        }

        protected override IQueryable<Article> EntitiesSource => context.Articles;

        protected override IQueryable<LinkRatingEntity<Article>> RatingsSource => context.LinkRatingArticle;

        protected override void SaveChangedState(Article changedEntity)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="id">The article identifier.</param>
        public override async Task DeleteEntity(Guid id)
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
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override async Task SaveEntity(Article entity)
        {
            Article dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                // add nonexisting tags
                var allTags = context.Tags.ToList();
                var existingTags = allTags.Where(tag => entity.Info.EFTags.Any(eftag => eftag.Tag.Value.ToLower() == tag.Value.ToLower()));
                var missingTags = entity.Info.EFTags.Select(link => link.Tag).Where(eftag => !existingTags.Any(tag => tag.Value.ToLower() == eftag.Value.ToLower()));

                context.AddRange(missingTags);

                // remove old links
                context.RemoveRange(dbEntry.Info.EFTags);

                // set tag ids of new links
                var linkTagsWithIdExisting = existingTags.Select(tag => new LinkArticleTag() { Tag = tag, ArticleInfo = entity.Info }).ToList();
                var linkTagsWithIdAdded = missingTags.Select(tag => new LinkArticleTag() { Tag = tag, ArticleInfo = entity.Info }).ToList();

                linkTagsWithIdExisting.AddRange(linkTagsWithIdAdded);

                entity.Info.EFTags = linkTagsWithIdExisting;

                entity.CopyTo(dbEntry);
            }
            else
            {
                entity.Info.Created = DateTime.Now;

                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }
    }
}
