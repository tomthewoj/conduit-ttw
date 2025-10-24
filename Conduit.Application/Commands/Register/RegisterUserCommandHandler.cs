using Conduit.Application.Interfaces;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Conduit.Application.Commands.Register;
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
            var user = new User(request.UserName, request.Email);
            var hashedPassword = _hasher.HashPassword(user, request.Password);
            user.SetPasswordHash(hashedPassword);
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync(ct); //maybe add email sender?

            return Unit.Value;
        }   
}