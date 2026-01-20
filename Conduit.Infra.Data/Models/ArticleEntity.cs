using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Conduit.Infra.Data.Models
{
    public class ArticleEntity
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public UserEntity Author { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<ArticleTagsEntity> ArticleTags { get; set; }
        public ICollection<ArticleFavoriteEntity> Favorited { get; set; }
    }
}
