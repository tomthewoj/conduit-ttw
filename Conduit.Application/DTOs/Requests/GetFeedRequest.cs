namespace Conduit.Application.DTOs.Requests
{
    public record GetFeedRequest(
        int limit = 20,
        int offset = 0
        );
}
