using MediatR;

namespace Conduit.Modules.Users.Application.Commands.Login
{
    public record LoginUserCommand(string Username, string Password) : IRequest<bool>; // You can create your own request type, later
}
