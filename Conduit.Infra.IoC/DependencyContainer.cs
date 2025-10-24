using Conduit.Application.Commands.Register;
using Conduit.Application.Interfaces;
using Conduit.Application.Queries.Register;
using Conduit.Application.Validation;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Repository;
using Conduit.Infra.IoC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Conduit.Application.Commands.Login;

namespace Conduit.Infra.IoC
{
    public static class DependencyContainer // consider automapper as well maybe?
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            services.AddMediatR(cfg =>
            {
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.RegisterServicesFromAssemblies(
                    typeof(RegisterUserCommandHandler).Assembly,
                    typeof(LoginUserCommandHandler).Assembly,
                    typeof(CheckEmailExistsHandler).Assembly,
                    typeof(CheckUserExistsHandler).Assembly);
            });
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
            return services;
        }
    }
}
