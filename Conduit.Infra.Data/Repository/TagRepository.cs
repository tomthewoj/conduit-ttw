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
    public class TagRepository :ITagRepository
    {
        private readonly ConduitDbContext _context;
        public TagRepository(ConduitDbContext context) => _context = context;

        public async Task<ICollection<Tag>> AddAndReturnTags(ICollection<string> tags, CancellationToken ct = default)
        {
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

            var allTags = existingTags.Concat(missingTags).Select(t => new Tag(t.Id, t.Name)).ToList();
            return allTags;
        }
        public async Task<ICollection<Tag>> GetTags(CancellationToken ct = default)
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

        public Task AddTag(Guid articleId, string tag)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTag(Guid articleId, string tag)
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