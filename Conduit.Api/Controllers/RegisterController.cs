using Conduit.Application.Commands.Register;
using Conduit.Application.Queries.ActiveUsers;
using Conduit.Application.Queries.Register;
using Conduit.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto.Username, dto.Email, dto.Password);

            try
            {
                var userId = await _mediator.Send(command); // var userId = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction(nameof(GetUser), new { id = userId }, new { id = userId, dto.Username });
            }
            catch (FluentValidation.ValidationException ex)
            {
                // Extract property errors
                var errors = ex.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList();

                // Return 400 with errors
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok();
        }
        [HttpGet("userExists")]
        public async Task<IActionResult> UserExists(string userName)
        {
            var query = new CheckUserExistsQuery(userName);
            try
            {
                var exists = await _mediator.Send(query);
                return Ok(new{ exists }); // json looks prettier
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("emailExists")]
        public async Task<IActionResult> EmailExists(string email)
        {
            var query = new CheckEmailExistsQuery(email);
            try
            {
                var exists = await _mediator.Send(query);
                return Ok(new { exists }); // json looks prettier
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("all-usernames")]
        [Authorize]
        public async Task<IActionResult> GetAllUserNames()
        {
            var query = new CheckActiveUsersQuery();
            var users = await _mediator.Send(query); // returns domain Users
            var usernames = users.Select(u => u.UserName);
            return Ok(usernames);
        }
    }
}
