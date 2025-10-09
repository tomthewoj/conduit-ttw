using Conduit.Modules.Users.Domain.Entities;
using Conduit.Modules.Users.Domain.Interfaces;
using Conduit.Modules.Users.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Conduit.Modules.Users.Application.Commands.Register;
public class RegisterUserHandler
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher _hasher;
    public RegisterUserHandler(IUserRepository repo, IPasswordHasher hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        //existing getemailasync if existing regisetered
        var user = new User(request.UserName, request.Email, _hasher.Hash(request.Password));
        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync(); //maybe add email sender?
        return user.Id;
    }
}