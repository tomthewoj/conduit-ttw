using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Interfaces
{
    public interface IFollowingRepository
    {
        Task<bool> IsUserFollowing(Guid userId, Guid followee);
        Task FollowUser(Guid guid, Guid followee);
        Task UnfollowUser(Guid userId, Guid followee);
    }
}
