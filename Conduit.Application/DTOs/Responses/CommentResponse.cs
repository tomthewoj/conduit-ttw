namespace Conduit.Application.DTOs.Responses
{
    public record CommentResponse
    (
        Guid Id,
        DateTime createdAt,
        DateTime updatedAt,
        string body,
        ProfileResponse author
    );
}
