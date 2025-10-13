using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Conduit.Modules.Users.Domain.Services
{
    public class PasswordHasher : IPasswordHasher // asp net identity, identity user zamiast usera
    {
        public string Hash(User user, string password)
        {
            var hasher = new PasswordHasher<User>(); // hasher.VerifyHashedPassword(user, user.PasswordHash, enteredPassword); for later use when logging in
            return hasher.HashPassword(user, password);
        }
    }
}
