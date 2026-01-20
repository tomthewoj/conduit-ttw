using Conduit.Application.DTOs.Responses;

namespace Conduit.Application.DTOs.Responses.Multiple
{
    public record ListArticleItemsResponse
    (
        List<SimpleArticleResponse> Articles,
        int ArticlesCount
    );
}
