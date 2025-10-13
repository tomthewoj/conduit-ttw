namespace Conduit.Modules.Users.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email);
    }
}
