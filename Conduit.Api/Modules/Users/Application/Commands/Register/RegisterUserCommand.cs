using MediatR;
using System;
namespace Conduit.Modules.Users.Application.Commands.Register;
public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest<Guid>
{
}