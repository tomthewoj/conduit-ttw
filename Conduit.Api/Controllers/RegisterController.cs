using Conduit.Modules.Users.Application.Commands.Register;
using Conduit.Modules.Users.Application.DTOs;
using Conduit.Modules.Users.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Conduit.Modules.Users.API.Controllers
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
            catch (InvalidOperationException ex) //maybe do my own exceptions
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // i'll try getting user by id later
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok();
        }
    }
}
