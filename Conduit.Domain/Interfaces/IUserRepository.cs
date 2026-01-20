using Conduit.Domain.Entities;
using Conduit.Domain.ValueObjects;

namespace Conduit.Domain.Interfaces;
public interface IUserRepository
{
    Task SaveChangesAsync(CancellationToken ct);

    Task Registration(User user, PasswordHash password);
    Task<User> GetAuthUser(string username, CancellationToken ct);

    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);

    Task<string> GetPasswordById(Guid id);

    Task UpdateUser(User user, PasswordHash password);
    Task UpdateUserProfile(UserProfile userProfile);


    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByUsername(string username);
    Task<UserProfile> GetUserProfile(string username);
    Task<IReadOnlyDictionary<Guid, User>> GetUsersByIds(IEnumerable<Guid> ids);
}