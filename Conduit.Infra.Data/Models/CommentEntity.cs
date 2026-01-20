using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Models
{
    public class CommentEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid AuthorId { get; set; }
        public required Guid ArticleId { get; set; }
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //navs
        public UserEntity Author { get; set; }
        public ArticleEntity Article { get; set; }
    }
}
