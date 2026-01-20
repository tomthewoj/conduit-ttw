namespace Conduit.Application.DTOs.Responses
{
    public record CommentResponse
    (
        DateTime createdAt,
        DateTime updatedAt,
        string body,
        ProfileResponse author
    );
}
