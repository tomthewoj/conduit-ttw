using Conduit.Domain.Entities;

namespace Conduit.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hash(User user, string password);
    }
}
