using Conduit.Modules.Users.Application.Commands.Register;
using Conduit.Modules.Users.Domain.Interfaces;
using Conduit.Modules.Users.Domain.Services;
using Conduit.Modules.Users.Infrastructure.Persistence;
using Conduit.Modules.Users.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Modules.Users.Infrastructure
{
    public static class DependencyContainer // consider automapper as well maybe?
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly)); // is paid now
            return services;
        }
    }
}
