namespace Conduit.Application.DTOs.Responses
{
    public record SimpleArticleResponse
    (
        string Slug,
        string Title,
        string Description,
        TagResponse TagList,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool Favorited,
        int FavoritesCount,
        ProfileResponse Author
    );
}
