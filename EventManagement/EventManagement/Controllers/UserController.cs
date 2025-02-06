using Application.Users.Command;
using Application.Users.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var createUser = await _mediator.Send(command);

            if (createUser == null) 
            {
                return BadRequest(new { Messaje = "No se pudo crear el evento."});
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createUser.UserId }, createUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(id));

                if (user == null)
                {
                    return NotFound(new { Message = $"El usuario con ID {id} no fue encontrado." });
                }

                return Ok(user);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { Message = "Error al obtener el usuario.", Details = ex.Message });
            }
            
        }
    }
}
