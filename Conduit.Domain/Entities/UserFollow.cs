using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class UserFollow
    {

        public UserFollow (Guid followerId, Guid followeeId) // consider a private one as well
        {
            FollowerId = followerId;
            FolloweeId = followeeId;
        }
        public Guid FollowerId { get; private set; }
        public Guid FolloweeId { get; private set; }
    }
}
