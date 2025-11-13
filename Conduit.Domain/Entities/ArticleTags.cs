using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class ArticleTags
    {
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
    }
}
