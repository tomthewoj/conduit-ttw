using Conduit.Application.Commands.Login;
using Conduit.Application.DTOs;
using Conduit.Application.Interfaces;
using Conduit.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            var command = new LoginUserCommand(request.Username, request.Password);
            var token = await _mediator.Send(command);
            return Ok(new { token });
        }
    }
}
