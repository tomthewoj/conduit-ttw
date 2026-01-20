using Conduit.Application.Interfaces;
using Conduit.Domain.Interfaces;
using Conduit.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        IUserRepository _repo;
        IPasswordHasherService _hasher;
        public UpdateUserCommandHandler(IUserRepository repo, IPasswordHasherService hasher)
        {
            _repo = repo;
            _hasher = hasher;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            /*
            var requestData = request.User;
            var currentUser = await _repo.GetUserById(requestData.UserId);
            var currentUserProfile = currentUser.Profile;
            PasswordHash passwordHashVo = null;


            if (requestData.Email != null) currentUser.UpdateEmail(requestData.Email);
            if (requestData.UserName != null) currentUser.UpdateUserName(requestData.UserName);
            if (requestData.Password != null)
            {
                var hashedPassword = _hasher.HashPassword(currentUser, requestData.Password);
                passwordHashVo = PasswordHash.Create(hashedPassword);
            }

            if (requestData.Image != null) currentUserProfile.ImageLink = requestData.Image;
            if (requestData.Bio != null) currentUserProfile.Bio = requestData.Bio;
            await _repo.UpdateUserProfile(currentUserProfile);
            await _repo.UpdateUser(currentUser, passwordHashVo);
            
            */
            return Unit.Value;
        }
    }
}
