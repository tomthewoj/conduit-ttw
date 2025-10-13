using Conduit.Modules.Users.Domain.Entities;
using Conduit.Modules.Users.Domain.Interfaces;
using Conduit.Modules.Users.Infrastructure.Persistence.Entities;

namespace Conduit.Modules.Users.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) => _context = context;
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
        
        public async Task AddAsync(User user)
        {
            var entity = MapToEntity(user);
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }
        //now logging in 
        private UserEntity MapToEntity(User user) => new UserEntity
        {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
         }; // convenient
    }
}
