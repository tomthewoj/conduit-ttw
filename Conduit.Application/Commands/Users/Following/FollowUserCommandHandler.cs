using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Users.Following
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand,Unit>
    {
        IUserRepository _userRepository;
        IFollowingRepository _followingRepository;
        public FollowUserCommandHandler(IUserRepository userRepository, IFollowingRepository folowingRepository)
        {
            _userRepository = userRepository;
            _followingRepository = folowingRepository;
        }
        public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetUserById(request.currentUserId);
            var followeeUser = await _userRepository.GetUserByUsername(request.followee);
            await _followingRepository.FollowUser(currentUser.Id, followeeUser.Id);
            return Unit.Value;
        }
    }
}
