namespace Conduit.Modules.Users.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
    }
}
