using Conduit.Modules.Users.Domain.Entities;
using Conduit.Modules.Users.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Modules.Users.Infrastructure.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext( DbContextOptions<UserDbContext> options) : base (options) { }
        public DbSet<UserEntity> Users { get; set; } = null;
    }
}
