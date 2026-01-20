using Conduit.Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Users
{
    public record class GetProfileQuery(Guid? currentUserId, string username) : IRequest<ProfileResponse>;
}
