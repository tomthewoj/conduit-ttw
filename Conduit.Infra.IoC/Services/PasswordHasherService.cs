using Conduit.Application.Interfaces;
using Conduit.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Conduit.Infra.IoC.Services
{
    public class PasswordHasherService : IPasswordHasherService // asp net identity, identity user zamiast usera
    {
        private readonly IPasswordHasher<User> _hasher;
        public PasswordHasherService(IPasswordHasher<User> hasher) => _hasher = hasher;
        public string HashPassword(User user, string password) =>
        _hasher.HashPassword(user, password);
        public bool VerifyPassword(User user, string password, string hashed) =>
            _hasher.VerifyHashedPassword(user, hashed, password) == PasswordVerificationResult.Success; // implement fully
    }
}

