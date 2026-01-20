using Conduit.Application.Queries.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/side")]
    public class RegisterController : Controller
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
