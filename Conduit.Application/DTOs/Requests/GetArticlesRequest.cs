namespace Conduit.Application.DTOs.Requests
{
    public record GetArticlesRequest
    (
      string tag = "",
      string author = "",
      string favorited = "",
      int limit = 20,
      int offset = 0
    );
}
