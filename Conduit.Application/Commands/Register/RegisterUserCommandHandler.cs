using Conduit.Modules.Users.Domain.Entities;
using Conduit.Modules.Users.Domain.Interfaces;
using Conduit.Modules.Users.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace Conduit.Modules.Users.Application.Commands.Register;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher _hasher;
    public RegisterUserCommandHandler(IUserRepository repo, IPasswordHasher hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken ct = default)
        {
            //existing getemailasync if existing regisetered
            var user = new User(request.UserName, request.Email);
            var hashedPassword = _hasher.Hash(user, request.Password);
            user.SetPasswordHash(hashedPassword);
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync(ct); //maybe add email sender?
            return user.Id;
        }
}