using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infra.Data.Context
{
    public class ConduitDbContext : DbContext
    {
        public ConduitDbContext(DbContextOptions<ConduitDbContext> options) : base(options) { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserFollowEntity> UserFollow { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserFollowEntity>(entity =>
            {
                entity.HasKey(e => new { e.FollowerId, e.FolloweeId });
            });

        }
    }
}
