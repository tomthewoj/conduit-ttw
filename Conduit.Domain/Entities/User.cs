using System;
namespace Conduit.Domain.Entities;
public class User(string username, string email)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string UserName { get; private set; } = username;
    public string? PasswordHash { get; private set; } // nullable probably isn't the best approach
    public string Email { get; private set; } = email;
    public DateTime CreatedDate { get; private set; } = DateTime.Now;

    public void SetPasswordHash(string password)
    {
        PasswordHash = password;
    }
}