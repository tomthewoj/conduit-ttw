using MediatR;
using System;
namespace Conduit.Application.Commands.Users.Register;
public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest<Unit> { }
