using Conduit.Domain.ValueObjects;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Conduit.Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public UserProfile Profile { get; private set; }
    public void UpdateUserName(string userName)
    {
        UserName = userName;
    }
    public void UpdateEmail(string email)
    {
        Email = email;
    }
    private User(Guid id, string userName, string email, UserProfile profile) //user creation
    {
        Id = id;
        UserName = userName;
        Email = email;
        Profile = profile;

    }
    private User(Guid id, string userName, string email ) //user creation
    {
        Id = id;
        UserName = userName;
        Email = email;
    }
    private User(Guid id, string userName, string email, UserProfile? profile, DateTime createdDate) //user creation
    {
        Id = id;
        UserName = userName;
        Email = email;
        if(profile != null) Profile = profile;
        CreatedDate = createdDate;
    }
    public static User CreateUser(string username, string email)
    {
        var id = Guid.NewGuid();
        var profile = new UserProfile(id, "", "");
        return new User(id, username, email, profile);
    }
    public static User LoadUser(
        Guid id,
        string username,
        string email)
    {
        return new User(id, username, email);
    }
    public static User LoadUser(
    Guid id,
    string userName,
    string email,
    DateTime createdDate,
    UserProfile? profile)
    {
        var user = new User(id, userName, email, profile, createdDate);
        return user;
    }
}