namespace Conduit.Application.DTOs.Responses
{
    public record ProfileResponse
    (
        string username,
        string bio,
        string image,
        bool following
    );
}
