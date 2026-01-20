namespace Conduit.Application.DTOs.Responses
{
    public record TagResponse
    (
        IReadOnlyCollection<string> tags
    );
}
