using Conduit.Application.Interfaces;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Conduit.Application.Commands.Login
{
    public class LoginUserCommandHandler(
        IUserRepository userRepo,
        IPasswordHasher<User> hasher,
        ITokenService jwtTokenService) : IRequestHandler<LoginUserCommand,string>
    {
        public async Task<string> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var user = await userRepo.GetByUserNameAsync(request.Username, ct);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid credentials");

            // Generate JWT token
            var token = jwtTokenService.GenerateToken(user.Id.ToString(), user.Email);
            return token;
        }
    }
}
