using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Guid commentId);
        Task<IReadOnlyCollection<Comment>> GetAllComments(Guid articleId, int limit, int offset);
    }
}
