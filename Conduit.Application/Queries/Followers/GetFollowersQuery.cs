using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Followers
{
    public record GetFollowersQuery(Guid userId) : IRequest<IEnumerable<FollowerDTO>>;
    public record FollowerDTO(Guid id, string Username);
}
