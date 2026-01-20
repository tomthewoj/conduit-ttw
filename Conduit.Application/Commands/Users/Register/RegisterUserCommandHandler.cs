using Conduit.Application.Interfaces;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Domain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Conduit.Application.Commands.Users.Register;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasherService _hasher;
    public RegisterUserCommandHandler(IUserRepository repo, IPasswordHasherService hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken ct = default)
        {
        //existing getemailasync if existing regisetered
            var user = User.CreateUser(request.UserName, request.Email);
            var hashedPassword = PasswordHash.Create(_hasher.HashPassword(user, request.Password));

            await _repo.Registration(user, hashedPassword);
            await _repo.SaveChangesAsync(ct); //maybe add email sender?

            return Unit.Value;
        }   
}