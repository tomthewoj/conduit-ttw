using Conduit.Domain.Entities;

namespace Conduit.Domain.Interfaces;
public interface IUserRepository
{
    public Task AddAsync(User user);
    public Task SaveChangesAsync(CancellationToken ct);
}