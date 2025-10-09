namespace Conduit.Modules.Users.Infrastructure.Persistence.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null;
        public string Email { get; set; } = null;
        public string PasswordHash { get; set; } = null;
    }
}
