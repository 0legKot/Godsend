// <copyright file="RatingHelper.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRatingHelper
    {
        Task<double> CalculateAverageAsync<TLink>(DbSet<TLink> linkSet, Guid entityId)
            where TLink : LinkRatingEntity;

        /*Task SaveAverageAsync<TEntity>(DbSet<TEntity> entitySet, Guid entityId, double avg, DataContext context)
            where TEntity : class, IEntity;*/

        Task SetRatingAsync<TLink>(Guid entityId, string userId, int rating, DbSet<TLink> linkSet, DataContext context)
            where TLink : LinkRatingEntity, new();
    }

    public class RatingHelper : IRatingHelper
    {
        public async Task<double> CalculateAverageAsync<TLink>(DbSet<TLink> linkSet, Guid entityId)
            where TLink : LinkRatingEntity
        {
            var ratingsExist = await linkSet.AnyAsync(link => link.EntityId == entityId);

            var avg = ratingsExist
                ? await linkSet
                    .Where(link => link.EntityId == entityId)
                    .Select(link => link.Rating)
                    .AverageAsync()
                : 0;

            return avg;
        }

       /* public async Task SaveAverageAsync<TEntity>(DbSet<TEntity> entitySet, Guid entityId, double avg, DataContext context)
            where TEntity : class, IEntity
        {
            var entity = await entitySet.FirstOrDefaultAsync(e => e.Id == entityId);
            entity.Info.Rating = avg;

            await context.SaveChangesAsync();
        }*/

        public async Task SetRatingAsync<TLink>(Guid entityId, string userId, int rating, DbSet<TLink> linkSet, DataContext context)
            where TLink : LinkRatingEntity, new()
        {
            var existingRating = await linkSet.FirstOrDefaultAsync(link => link.UserId == userId && link.EntityId == entityId);

            if (existingRating == null)
            {
                var newRating = new TLink { UserId = userId, Rating = rating, EntityId = entityId };
                context.Add(newRating);
            }
            else
            {
                existingRating.Rating = rating;
            }

            await context.SaveChangesAsync();
        }
    }
}
