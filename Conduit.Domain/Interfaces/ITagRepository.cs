using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> AddAndReturnTags(ICollection<string> tags, CancellationToken ct = default);
        Task<ICollection<string>> GetTagNamesByIds(ICollection<Guid> tags, CancellationToken ct = default);
        Task<IReadOnlyCollection<string>> GetTagsForArticle(Guid articleId);
        Task<IReadOnlyDictionary<Guid, List<string>>> GetTagsForArticles(ICollection<Guid> articleIds);
        Task AddTag(Guid articleId, string tag);
        Task RemoveTag(Guid articleId, string tag);
    }
}
