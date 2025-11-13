using Conduit.Application.Commands.Login;
using Conduit.Application.Interfaces;
using Conduit.Application.Queries.ActiveUsers;
using Conduit.Application.Queries.Followers;
using Conduit.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    public class FollowerController : Controller
    {
        private readonly IMediator _mediator;
        public FollowerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("followers")]
        public async Task<IActionResult> GetFollowers([FromBody] FollowersDto request)
        {
            var query = new GetFollowersQuery(request.Id);
            var users = await _mediator.Send(query); // returns domain Users
            return Ok(users);
        }
    }
}
