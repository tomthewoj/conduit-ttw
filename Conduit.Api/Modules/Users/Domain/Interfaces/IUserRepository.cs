using Conduit.Modules.Users.Domain.Entities;
namespace Conduit.Modules.Users.Domain.Interfaces;
public interface IUserRepository
{
    public Task AddAsync(User user);
    public Task SaveChangesAsync();
}