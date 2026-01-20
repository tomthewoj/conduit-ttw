using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddFavorite(Guid articleId, Guid userId);
        Task RemoveFavorite(Guid articleId, Guid userId);
        Task<int> GetFavoritesCount(Guid articleId);
        Task<bool> IsFavoritedByUser(Guid articleId, Guid userId);
        Task<IReadOnlyDictionary<Guid,int>> GetFavoritesCountForArticles(ICollection<Guid> articleIds); // Dictionary<ArticleId, int>
        Task<IReadOnlyDictionary<Guid,bool>> IsFavoritedByUserForArticles(ICollection<Guid> articleIds, Guid currentUserId);
    }
}
