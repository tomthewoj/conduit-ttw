using Conduit.Application.DTOs.Responses;
using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Users
{
    public record GetUserQuery(Guid Id, string token) : IRequest<UserResponse>;
}
