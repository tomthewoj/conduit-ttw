using Conduit.Application.Queries.ActiveUsers;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Followers
{
    public class GetFollowersHandler(IUserRepository userRepo, IFollowerRepository followerRrepo) : IRequestHandler<GetFollowersQuery, IEnumerable<FollowerDTO>>
    {
        private readonly IUserRepository _urepo = userRepo;
        private readonly IFollowerRepository _frepo = followerRrepo;

        public async Task<IEnumerable<FollowerDTO>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            var followeeId = request.userId;
            var followerIds = await _frepo.GetAllFollowers(followeeId);
            var followers = await _urepo.GetUsersById(followerIds);
            return followers.Select(u => new FollowerDTO(u.Id, u.UserName));
        }
    }
}
