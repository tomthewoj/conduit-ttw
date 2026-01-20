using Conduit.Application.DTOs.Responses;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Users
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery,ProfileResponse>
    {
        IUserRepository _userRepository;
        IFollowingRepository _followingRepository;
        public GetProfileHandler(IUserRepository userRepository, IFollowingRepository followingRepository)
        {
            _followingRepository = followingRepository;
            _userRepository = userRepository;
        }

        public async Task<ProfileResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(request.username);
            if (user == null) throw new Exception("User doesn't exist");
            bool isFollowing = request.currentUserId != null ? await _followingRepository.IsUserFollowing((Guid)request.currentUserId, user.Id) : false;
            var resultProfie = new ProfileResponse(request.username,user.Profile.Bio,user.Profile.ImageLink,isFollowing);
            return resultProfie;
        }
    }
}
