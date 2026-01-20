namespace Conduit.Application.DTOs.Requests
{
    public record UpdateArticleRequest(
        string? Title,
        string? Description,
        string? Body
        );
}
