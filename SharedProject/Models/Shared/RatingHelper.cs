using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface IRatingHelper
    {
        Task<double> CalculateAverage<TLink>(DbSet<TLink> linkSet, Func<TLink, Guid> idSelector, Guid entityId)
            where TLink : LinkRatingEntity;

        Task SaveAverage<TEntity>(DbSet<TEntity> entitySet, Guid entityId, double avg, DataContext context)
            where TEntity : class, IEntity;

        Task SetRating<TLink>(Guid entityId, string userId, int rating, DbSet<TLink> linkSet, Func<TLink, Guid> idSelector, DataContext context)
            where TLink : LinkRatingEntity, new();
    }

    public class RatingHelper : IRatingHelper
    {
        public async Task<double> CalculateAverage<TLink>(DbSet<TLink> linkSet, Func<TLink, Guid> idSelector, Guid entityId)
            where TLink : LinkRatingEntity
        {
            var ratingsExist = await linkSet.AnyAsync(link => idSelector(link) == entityId);

            var avg = ratingsExist
                ? await linkSet
                    .Where(link => idSelector(link) == entityId)
                    .Select(link => link.Rating)
                    .AverageAsync()
                : 0;

            return avg;
        }

        public async Task SaveAverage<TEntity>(DbSet<TEntity> entitySet, Guid entityId, double avg, DataContext context)
            where TEntity : class, IEntity
        {
            var entity = await entitySet.Include(e => e.Info).FirstOrDefaultAsync(e => e.Id == entityId);
            entity.Info.Rating = avg;

            await context.SaveChangesAsync();
        }

        public async Task SetRating<TLink>(Guid entityId, string userId, int rating, DbSet<TLink> linkSet, Func<TLink, Guid> idSelector, DataContext context)
            where TLink : LinkRatingEntity, new()
        {
            var existingRating = await linkSet.FirstOrDefaultAsync(link => link.UserId == userId && idSelector(link) == entityId);

            if (existingRating == null)
            {
                var newRating = new TLink { UserId = userId, Rating = rating };
                newRating.SetEntityId(entityId);
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
