using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Conduit.Infra.Data.Repository
{
    public class TagRepository :ITagRepository
    {
        private readonly ConduitDbContext _context;
        public TagRepository(ConduitDbContext context) => _context = context;

        public async Task<ICollection<Tag>> GetAllTags(CancellationToken ct = default)
        {
            return await _context.Tags.Select(t => new Tag(t.Id, t.Name)).ToListAsync();
        }
        public async Task<ICollection<string>> GetTagNamesByIds(ICollection<Guid> tags, CancellationToken ct = default)
        {
            return await _context.Tags.Where(t => tags.Contains(t.Id)).Select(t => t.Name).ToListAsync();
        }
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<string>> GetTagsForArticle(Guid articleId)
        {
            return await _context.ArticleTags.Where(t => t.ArticleId == articleId).Select(t => t.Tag.Name).ToListAsync();
        }

        public async Task<IReadOnlyDictionary<Guid, List<string>>> GetTagsForArticles(ICollection<Guid> articleIds)
        {
            var result =  new Dictionary<Guid, List<string>>();
            foreach(var articleId in articleIds)
            {
                var tags = await _context.ArticleTags.Where(t => t.ArticleId == articleId).Select(t => t.Tag.Name).ToListAsync();
                result.Add(articleId, tags);
            }
            return result;
        }

        public async Task AddTags(Guid articleId, ICollection<string> tags, CancellationToken ct = default)
        {
            var normalizedNames = tags.Select(t => t.Trim().ToLower()).Where(t => !string.IsNullOrWhiteSpace(t)).Distinct().ToList();
            var existingTags = await _context.Tags.Where(t => normalizedNames.Contains(t.Name)).ToListAsync(ct);
            var existingTagNames = existingTags.Select(t => t.Name).ToHashSet();

            var missingTags = normalizedNames.Where(name => !existingTagNames.Contains(name)).Select(name => new TagEntity{
                    Id = Guid.NewGuid(),
                    Name = name
                }).ToList();
            await _context.Tags.AddRangeAsync(missingTags, ct);

            var allTags = existingTags.Concat(missingTags).ToList();
            var existingArticleTagIds = await _context.ArticleTags.Where(at => at.ArticleId == articleId).Select(at => at.TagId).ToListAsync(ct); //hashset broke
            var articleTags = allTags.Where(t => !existingArticleTagIds.Contains(t.Id)).Select(t => new ArticleTagsEntity{
                    ArticleId = articleId,
                    TagId = t.Id
                });
            await _context.ArticleTags.AddRangeAsync(articleTags, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task RemoveTags(Guid articleId, ICollection<string> tag, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}

/*
           var normalizedNames = tags.Select(t => t.Trim().ToLower()).Distinct().ToList();
            var existingTags = await _context.Tags.Where(t => normalizedNames.Contains(t.Name)).ToListAsync(ct);
            var existingTagNames = existingTags.Select(t => t.Name); // hashset obczaj aby przyśpieszyć w razie czego

            var missingTags = normalizedNames.Where(name => !existingTagNames.Contains(name)).Select(name => new TagEntity
            {
                Id = Guid.NewGuid(),
                Name = name
            }).ToList();
            await _context.Tags.AddRangeAsync(missingTags);
            await _context.SaveChangesAsync(ct);

            List<Guid> existingIds = existingTags.Select(t => t.Id).ToList();
            List<Guid> missingIds = missingTags.Select(t => t.Id).ToList();
            existingIds.AddRange(missingIds);
            return existingIds;

*/