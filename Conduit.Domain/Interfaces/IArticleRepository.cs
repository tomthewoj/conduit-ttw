using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Interfaces
{
    public interface IArticleRepository
    {
        Task CreateArticle(Article article, IReadOnlyCollection<string> tags);
        Task UpdateArticle(Article article);
        Task DeleteArticle(Guid articleId);

        Task AddComment(Comment comment);
        Task DeleteComment(Guid commentId);
        Task<ICollection<Comment>> GetComments(Guid articleId);

        Task<Article?> GetArticleBySlug(string slug);
        Task<Guid?> GetArticleIdBySlug(string slug);

        Task<(IReadOnlyList<Article>,int)> ListArticles(string tag, string author, string favorited, int limit, int offset);
        Task<(IReadOnlyList<Article>, int)> FeedArticles(Guid currentUserId, int limit, int offset);
    }
}
