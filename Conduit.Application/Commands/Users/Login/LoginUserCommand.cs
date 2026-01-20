using MediatR;

namespace Conduit.Application.Commands.Users.Login
{
    public record LoginUserCommand(string Username, string Password) : IRequest<string>; // You can create your own request type, later
}
