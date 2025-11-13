using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Models
{
    public class UserFollowEntity
    {
        public Guid FollowerId { get;  set; }
        public Guid FolloweeId { get;  set; }
    }
}
