namespace Conduit.Application.DTOs.Responses
{
    public record ArticleResponse
    (
        string slug,
        string title,
        string description,
        string body,
        IReadOnlyCollection<string> tagList,
        DateTime createdAt,
        DateTime updatedAt,
        bool favorited,
        int favoritesCount,
        ProfileResponse author
    );
    //instead of tag response
}
