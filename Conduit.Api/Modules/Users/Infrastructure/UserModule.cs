using Conduit.Modules.Users.Application.Commands.Register;
using Conduit.Modules.Users.Domain.Interfaces;
using Conduit.Modules.Users.Infrastructure.Persistence;
using Conduit.Modules.Users.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Modules.Users.Infrastructure
{
    public static class UserModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly)); // consider automapper as well maybe?
            return services;
        }
    }
}
