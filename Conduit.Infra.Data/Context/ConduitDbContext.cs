using Conduit.Domain.Entities;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infra.Data.Context
{
    public class ConduitDbContext : DbContext
    {
        public ConduitDbContext(DbContextOptions<ConduitDbContext> options) : base(options) { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<UserProfileEntity> UserProfiles { get; set; }
        public DbSet<UserFollowEntity> UserFollow { get; set; }

        public DbSet<ArticleFavoriteEntity> ArticleFavorite { get; set; }
        public DbSet<ArticleTagsEntity> ArticleTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) //cascade vs restrict
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(userEntity =>
            {
                userEntity.HasKey(pk => pk.Id);
                userEntity.HasMany(a => a.Articles)
                          .WithOne(a => a.Author)
                          .HasForeignKey(a => a.AuthorId).OnDelete(DeleteBehavior.Cascade);

                userEntity.HasMany(c => c.Comments)
                          .WithOne(c => c.Author)
                          .HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.Cascade);

                userEntity.HasMany(c => c.Favorites)
                          .WithOne(f => f.Author)
                          .HasForeignKey(f => f.AuthorId).OnDelete(DeleteBehavior.Cascade);

                userEntity.HasOne(u => u.Profile)
                          .WithOne()                     
                          .HasForeignKey<UserProfileEntity>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ArticleEntity>(articleEntity =>
            {
                articleEntity.HasKey(pk => pk.Id);

                articleEntity.HasMany(c => c.Comments)
                             .WithOne(c => c.Article)
                             .HasForeignKey(c => c.ArticleId);

                articleEntity.HasMany(c => c.Favorited)
                             .WithOne(f => f.Article)
                             .HasForeignKey(f => f.ArticleId);

                articleEntity.HasMany(c => c.ArticleTags)
                             .WithOne(at => at.Article)
                             .HasForeignKey(at => at.ArticleId);

            });
            modelBuilder.Entity<CommentEntity>(entity =>
            {
                entity.HasKey(pk => pk.Id);
                entity.HasOne(c => c.Author)
                       .WithMany(u => u.Comments)
                       .HasForeignKey(c => c.AuthorId)
                       .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Article)
                      .WithMany(a => a.Comments)
                      .HasForeignKey(c => c.ArticleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasKey(pk => pk.Id);
            }); //can do max length and such, or unique names

            modelBuilder.Entity<UserProfileEntity>(userProfEntity =>
            {
                userProfEntity.HasKey(fk => fk.UserId);
            });
            modelBuilder.Entity<UserFollowEntity>(entity =>
            {
                entity.HasKey(ck => new { ck.FollowerId, ck.FolloweeId });

                entity.HasOne(uf => uf.Follower)
                      .WithMany(u => u.Following)
                      .HasForeignKey(uf => uf.FollowerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uf => uf.Followee)
                      .WithMany(u => u.Followers)
                      .HasForeignKey(uf => uf.FolloweeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ArticleFavoriteEntity>(entity =>
            {
                entity.HasKey(ck => new { ck.ArticleId, ck.AuthorId });

                entity.HasOne(af => af.Author)
                      .WithMany(u => u.Favorites)
                      .HasForeignKey(af => af.AuthorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(af => af.Article)
                      .WithMany(a => a.Favorited)
                      .HasForeignKey(af => af.ArticleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ArticleTagsEntity>(entity =>
            {
                entity.HasKey(ck => new { ck.ArticleId, ck.TagId });

                entity.HasOne(at => at.Article)
                      .WithMany(a => a.ArticleTags)
                      .HasForeignKey(at => at.ArticleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(at => at.Tag)
                      .WithMany(t => t.ArticleTags)
                      .HasForeignKey(at => at.TagId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            }
    }
}
