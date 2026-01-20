using Conduit.Application.Queries.Register;
using FluentValidation;
using MediatR;

namespace Conduit.Application.Commands.Users.Register
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator(IMediator mediator)
        {
            RuleFor(x => x.Email)
                .MustAsync(async (email, ct) =>
                {
                    var exists = await mediator.Send(new CheckEmailExistsQuery(email), ct);
                    return !exists;
                }).WithMessage("Email is already taken");
            RuleFor(x => x.UserName)
                .MustAsync(async (userName, ct) =>
                 {
                     var exists = await mediator.Send(new CheckUserExistsQuery(userName), ct);
                     return !exists;
                 }).WithMessage("Username is already taken");
        }
    }
}

