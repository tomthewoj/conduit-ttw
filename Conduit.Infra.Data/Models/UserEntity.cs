using Conduit.Domain.Entities;
using Conduit.Infra.Data.Models;

namespace Conduit.Infra.Data.Models
{
    public class UserEntity(Guid id, string userName, string email, string passwordHash)
    {
        public Guid Id { get; set; } = id;
        public string UserName { get; set; } = userName;
        public string Email { get; set; } = email;
        public string PasswordHash { get; set; } = passwordHash;
        public ICollection<ArticleEntity> Articles { get; set; }
        public ICollection<ArticleFavoriteEntity> Favorites { get; set; }
        public ICollection<UserFollowEntity> Followers { get; set; }
        public ICollection<UserFollowEntity> Following { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public UserProfileEntity Profile { get; set; }
    }
}
