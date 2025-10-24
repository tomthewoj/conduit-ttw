using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infra.Data.Repository
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
            await _context.Users.AddAsync(entity);
            await SaveChangesAsync();
        }
        //now logging in 
        private UserEntity MapToEntity(User user) => new UserEntity
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        }; // convenient

        public async Task<bool> ExistsByUsernameAsync(string username) => await _context.Users.AnyAsync(u => u.UserName == username);

        public async Task<bool> ExistsByEmailAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

        public async Task<User> GetByUserNameAsync(string username, CancellationToken ct = default)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (entity == null) return null;
            return new User(entity.Id, entity.UserName, entity.Email, entity.PasswordHash);
        }

        public async Task<IEnumerable<User>> GetAllActiveUsers()
        {
            var entities = await _context.Users.ToListAsync();
            return entities.Select(e => new User(e.Id, e.UserName, e.Email, e.PasswordHash));
        }
    }
}
