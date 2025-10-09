using Conduit.Modules.Users.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Conduit.Modules.Users.Domain.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            throw new NotImplementedException();
        }
    }
}
