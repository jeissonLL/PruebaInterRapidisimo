using Application.Events.Command;
using Application.EventUser.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (InvalidOperationException ex) // Excepciones controladas
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Otras excepciones no controladas
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}
