using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ConduitDbContext _context;
        public FavoriteRepository(ConduitDbContext context) => _context = context;
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task AddFavorite(Guid articleId, Guid userId)
        {
            var favorite = new ArticleFavoriteEntity();
            favorite.AuthorId = userId;
            favorite.ArticleId = articleId;
            _context.ArticleFavorite.Add(favorite);
            await SaveChangesAsync();
        }

        public async Task<int> GetFavoritesCount(Guid articleId)
        {
            return await _context.ArticleFavorite.Where(af => af.ArticleId == articleId).CountAsync();
        }

        public async Task<IReadOnlyDictionary<Guid, int>> GetFavoritesCountForArticles(ICollection<Guid> articleIds)
        {
            var result = new Dictionary<Guid, int>();
            foreach (var articleId in articleIds)
            {
                var count = await _context.ArticleFavorite.Where(af => af.ArticleId == articleId).CountAsync();
                result.Add(articleId, count);
            }
            return result;
        }

        public async Task<bool> IsFavoritedByUser(Guid articleId, Guid userId)
        {
            return await _context.ArticleFavorite.Where(a => a.ArticleId == articleId && a.AuthorId == userId).AnyAsync();
        }

        public async Task<IReadOnlyDictionary<Guid, bool>> IsFavoritedByUserForArticles(ICollection<Guid> articleIds, Guid userId)
        {
            var result = new Dictionary<Guid, bool>();
            foreach (var articleId in articleIds)
            {
                var favorited = await _context.ArticleFavorite.Where(a => a.ArticleId == articleId && a.AuthorId == userId).AnyAsync();
                result.Add(articleId, favorited);
            }
            return result;
        }

        public async Task RemoveFavorite(Guid articleId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
