using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class ArticleFollow
    {
        public Guid Userid { get; set; }
        public Guid ArticleId { get; set; }
    }
}
