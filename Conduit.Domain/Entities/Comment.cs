using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ArticleId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Comment(string body, Guid authorId, Guid articleId, DateTime createdAt, DateTime updatedAt)
        {
            Id = Guid.NewGuid();
            Body = body;
            AuthorId = authorId;
            ArticleId = articleId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
