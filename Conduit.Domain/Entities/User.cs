using System;
namespace Conduit.Domain.Entities;
public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string UserName { get; private set; }
    public string? PasswordHash { get; private set; } // nullable probably isn't the best approach
    public string Email { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public User(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
    public User(Guid id, string userName, string email, string passwordHash)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
    }


    public void SetPasswordHash(string password)
    {
        PasswordHash = password;
    }
}