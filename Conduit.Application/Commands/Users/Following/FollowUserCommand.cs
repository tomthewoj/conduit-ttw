using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Users.Following
{
    public record FollowUserCommand(Guid currentUserId, string followee) : IRequest<Unit>;
}
