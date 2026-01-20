using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Models
{
    public class TagEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArticleTagsEntity> ArticleTags { get; set; }
    }
}
