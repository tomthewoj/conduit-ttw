using Conduit.Domain.Entities;
using Conduit.Infra.Data.Models;
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
        public UserEntity Follower { get; private set; }
        public UserEntity Followee { get; private set; }
    }
}
