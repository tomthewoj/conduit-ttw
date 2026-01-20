namespace Conduit.Application.DTOs
{
    public record CreateArticleDto
    (
        string title,
        string description,
        string body,
        ICollection<string> tags
    );
}
