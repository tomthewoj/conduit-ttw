using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Models
{
    public class ArticleTagsEntity
    {
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
        public ArticleEntity Article { get; set; }
        public TagEntity Tag { get; set; }
    }
}
