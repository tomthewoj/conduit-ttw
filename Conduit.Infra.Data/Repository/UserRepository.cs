using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Domain.ValueObjects;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConduitDbContext _context;
        public UserRepository(ConduitDbContext context) => _context = context;
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }


        private UserEntity MapUser(User user, PasswordHash password) => new UserEntity(user.Id, user.UserName, user.Email, password.Value);

        public async Task<bool> ExistsByUsernameAsync(string username) => await _context.Users.AnyAsync(u => u.UserName == username);

        public async Task<bool> ExistsByEmailAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

        public async Task<string> GetPasswordById(Guid id)
        {
            var pass = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return pass.PasswordHash;
        }
        public async Task<User> GetAuthUser(string username, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return User.LoadUser(user.Id, user.UserName, user.Email);
        }


        public async Task Registration(User user, PasswordHash password)
        {
            var userEntity = MapUser(user, password);
            userEntity.Profile = new UserProfileEntity(userEntity.Id);
            await _context.Users.AddAsync(userEntity);
            await SaveChangesAsync();
        }
        public Task<UserProfile> GetCurrentUser(Guid? currentUserId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserProfile> GetUserProfile(string username)
        {
            var userProfile = await _context.Users.Include(u => u.Profile).Where(u => u.UserName == username)
                .Select(u => new UserProfile(u.Id, u.Profile.Bio, u.Profile.ImageLink)).FirstOrDefaultAsync();
            if (userProfile == null) throw new Exception("User not found");
            return userProfile;
        }

        public async Task UpdateUserProfile(UserProfile userProfile)
        {
            var dbProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserId == userProfile.UserId);
            if(userProfile.Bio != null) dbProfile.Bio = userProfile.Bio;
            if (userProfile.ImageLink != null)  dbProfile.ImageLink = userProfile.ImageLink;
            await SaveChangesAsync();
        }

        public async Task UpdateUser(User user, PasswordHash password)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if(user.UserName != null) dbUser.UserName = user.UserName;
            if (user.Email != null) dbUser.Email = user.Email;
            if (password != null) dbUser.PasswordHash = password.Value;

            await SaveChangesAsync();
        }

        public async Task FollowUser(Guid currentUserId, string followee)
        {
            var user = await _context.Users.Include(u => u.Following).FirstOrDefaultAsync(u => u.Id == currentUserId);

            var followeeId = await _context.Users.Where(u => u.UserName == followee).Select(u => u.Id).FirstOrDefaultAsync();

            if (user == null) throw new Exception("User not found");
            if (followeeId == Guid.Empty) throw new Exception("Followee not found");

            user.Following ??= new List<UserFollowEntity>();

            if (!user.Following.Any(f => f.FolloweeId == followeeId))
            {
                user.Following.Add(new UserFollowEntity
                {
                    FolloweeId = followeeId,
                    FollowerId = currentUserId
                });
            }
            await SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == id);
            var profile = new UserProfile(user.Id, user.Profile.Bio, user.Profile.ImageLink);
            return User.LoadUser(user.Id, user.UserName, user.Email, user.CreatedDate, profile);
        }
        public async Task<IReadOnlyDictionary<Guid, User>> GetUsersByIds(IEnumerable<Guid> ids)
        {
            var result = new Dictionary<Guid, User>();
            foreach (var id in ids)
            {
                var user = await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(i => i.Id == id);
                var profile = new UserProfile(user.Id, user.Profile.Bio, user.Profile.ImageLink);
                result.Add(id, User.LoadUser(user.Id, user.UserName, user.Email, user.CreatedDate, profile));
            }
            return result;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;
            return User.LoadUser(user.Id, user.UserName, user.Email, user.CreatedDate, new UserProfile(user.Id, user.Profile.Bio, user.Profile.ImageLink));
        }
    }
}
