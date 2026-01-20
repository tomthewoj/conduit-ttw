namespace Conduit.Application.DTOs.Responses
{
    public record UserResponse
    (
        string email,
        string token,
        string username,
        string bio,
        string image
    );
}
