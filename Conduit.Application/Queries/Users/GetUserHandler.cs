using Conduit.Application.DTOs.Responses;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Users
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserResponse> 
    {        
        public GetUserHandler(IUserRepository repo) => _repo = repo;
        IUserRepository _repo;

        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetUserById(request.Id);
            return new UserResponse(user.Email, request.token, user.UserName, user.Profile.Bio, user.Profile.ImageLink);
        }
    }
}
