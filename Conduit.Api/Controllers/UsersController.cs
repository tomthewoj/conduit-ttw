
using Conduit.Application.Commands.Users;
using Conduit.Application.Commands.Users.Following;
using Conduit.Application.Commands.Users.Login;
using Conduit.Application.Commands.Users.Register;
using Conduit.Application.DTOs;
using Conduit.Application.DTOs.Responses;
using Conduit.Application.Interfaces;
using Conduit.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpPost("users/login")]
        public async Task<IActionResult> Authentication([FromBody] LoginUserDto request)
        {
            var command = new LoginUserCommand(request.Username, request.Password);
            var token = await _mediator.Send(command);

            return Ok(new { token });
        }

        [HttpPost("users")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto.Username, dto.Email, dto.Password);

            try
            {
                var userId = await _mediator.Send(command); // var userId = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction(nameof(GetCurrentUser), new { id = userId }, new { id = userId, dto.Username });
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList();

                return BadRequest(new { errors });
            }
            catch (InvalidOperationException ex) //maybe do my own exceptions
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserResponse dto)
        {

            return Ok();
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader["Bearer ".Length..];
            var query = new GetUserQuery(currentUserId, token);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("profile/{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            Guid? currentUserId = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
            var query = new GetProfileQuery(currentUserId, username);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPost("profile/{username}/follow")]
        public async Task<IActionResult> FollowUser(string username)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new FollowUserCommand(Guid.Parse(currentUserId), username);
            var response = await _mediator.Send(command);
            Console.WriteLine("Followed");
            return Ok(response);
        }
        [HttpDelete("profile/{username}/follow")]
        public async Task<IActionResult> UnfollowUser(string username)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new UnFollowUserCommand(Guid.Parse(currentUserId), username);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
