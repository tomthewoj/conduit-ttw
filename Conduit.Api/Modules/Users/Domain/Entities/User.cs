using System;
namespace Conduit.Modules.Users.Domain.Entities;
public class User(string username, string email, string passwordHash)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string UserName { get; private set; } = username;
    public string PasswordHash { get; private set; } = passwordHash;
    public string Email { get; private set; } = email;
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
}