using Conduit.Application.DTOs.Responses;

namespace Conduit.Application.DTOs.Responses.Multiple
{
    public record ListCommentItemsResponse
    (
        List<CommentResponse> comments
    );
}
