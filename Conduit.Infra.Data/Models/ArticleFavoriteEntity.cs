using Conduit.Domain.Entities;
using Conduit.Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Models
{
    public class ArticleFavoriteEntity
    {
        public Guid AuthorId { get; set; } // favoriter not author
        public Guid ArticleId { get; set; }
        public UserEntity Author { get; set; }
        public ArticleEntity Article { get; set; }
    }
}
