using Conduit.Domain.Entities;

namespace Conduit.Domain.Interfaces;
public interface IUserRepository
{
    Task AddAsync(User user);
    Task SaveChangesAsync(CancellationToken ct);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string username, CancellationToken ct);
    Task<IEnumerable<User>> GetAllActiveUsers();
    Task<User> GetUserById(Guid id);
    Task<IEnumerable<User>> GetUsersById(IEnumerable<Guid> ids);
}