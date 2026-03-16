using Conduit.Application.Commands.Articles;
using Conduit.Application.Commands.Articles.Favorite;
using Conduit.Application.Commands.Comments;
using Conduit.Application.DTOs;
using Conduit.Application.DTOs.Requests;
using Conduit.Application.Interfaces;
using Conduit.Application.Queries.Articles;
using Conduit.Application.Queries.Comments;
using Conduit.Application.Queries.Tags;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api")]
    public class ArticleController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }
        [Authorize]
        [HttpPost("articles")]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var command = new CreateArticleCommand(currentUserId, dto.title, dto.description,dto.body, dto.tags );
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [Authorize]
        [HttpPut("articles/{slug}")]
        public async Task<IActionResult> UpdateArticle([FromBody] UpdateArticleRequest req, string slug)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var command = new UpdateArticleCommand(currentUserId, slug, req.Title, req.Description,req.Body);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("articles")]
        public async Task<IActionResult> ListArticles([FromQuery] GetArticlesRequest req)
        {
            Guid? currentUserId = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
            var query = new ListArticlesQuery(currentUserId, req.tag, req.author, req.favorited, req.limit, req.offset);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("articles/{slug}")]
        public async Task<IActionResult> GetArticle(string slug)
        {
            Guid? currentUserId = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
            var query = new GetArticleQuery(currentUserId, slug);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("articles/{slug}")]
        public async Task<IActionResult> DeleteArticle(string slug)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var command = new DeleteArticleCommand(slug, currentUserId);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("articles/feed")]
        public async Task<IActionResult> Feed([FromQuery] GetFeedRequest req)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var query = new FeedArticlesQuery(currentUserId, req.limit, req.offset);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("articles/{slug}/favorite")]
        public async Task<IActionResult> FavoriteArticle(string slug)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var command = new FavoriteArticleCommand(currentUserId,slug );
            var response =  await _mediator.Send(command);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("articles/{slug}/favorite")]
        public async Task<IActionResult> UnfavoriteArticle(string slug)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var command = new UnfavoriteArticleCommand(currentUserId,slug );
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("article/{slug}/comments")]
        public async Task<IActionResult> AddComment(string commentBody, string slug)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId)) return Unauthorized();

            var command = new AddCommentCommand(currentUserId, slug, commentBody);  
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("article/{slug}/comments")]
        public async Task<IActionResult> GetComments(string slug)
        {
            Guid? currentUserId = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
            var query = new GetCommentsQuery(currentUserId, slug);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("article/{slug}/comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(string slug, Guid commentId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var currentUserId))
                return Unauthorized();
            var command = new DeleteCommentCommand(currentUserId, commentId);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var query = new GetAllTagsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
