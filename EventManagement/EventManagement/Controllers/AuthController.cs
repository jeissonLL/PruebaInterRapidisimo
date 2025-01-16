using Application.DTO;
using Application.Login.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _mediator.Send(new LoginCommand(loginDto));
            if (!result)
                return Unauthorized("Invalid credentials");

            return Ok(new { message = "Login successful" });
        }
    }
}
