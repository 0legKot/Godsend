using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public interface ICommentHelper
    {
        Task DeleteCommentGenericAsync<TLink>(DbSet<TLink> linkSet, DataContext context, Guid entityId, Guid commentId)
            where TLink : LinkCommentEntity;

        Task<Guid> AddCommentGenericAsync<TLink>(DataContext context, Guid entityId, string userId, Guid? baseCommentId, string comment)
            where TLink : LinkCommentEntity, new();
    }

    public class CommentHelper : ICommentHelper
    {
        public async Task DeleteCommentGenericAsync<TLink>(DbSet<TLink> linkSet, DataContext context, Guid entityId, Guid commentId)
            where TLink : LinkCommentEntity
        {
            var entityComments = linkSet.Where(lce => lce.EntityId == entityId).ToArray();

            var commentToDelete = entityComments.FirstOrDefault(lce => lce.Id == commentId);

            deleteRecursive(commentToDelete);

            context.Remove(commentToDelete);

            await context.SaveChangesAsync();

            return;

            void deleteRecursive(LinkCommentEntity current)
            {
                foreach (var child in current.ChildComments)
                {
                    deleteRecursive(child);
                    context.Remove(child);
                }
            }
        }

        public async Task<Guid> AddCommentGenericAsync<TLink>(DataContext context, Guid entityId, string userId, Guid? baseCommentId, string comment)
            where TLink : LinkCommentEntity, new()
        {
            var newComment = new TLink
            {
                EntityId = entityId,
                UserId = userId,
                Comment = comment,
                BaseCommentId = baseCommentId
            };
            context.Add(newComment);
            await context.SaveChangesAsync();
            return newComment.Id;
        }
    }
}
