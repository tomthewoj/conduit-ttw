using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infra.Data.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext( DbContextOptions<UserDbContext> options) : base (options) { }
        public DbSet<UserEntity> Users { get; set; } = null;
    }
}
