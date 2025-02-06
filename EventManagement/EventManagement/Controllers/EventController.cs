using Application.Events.Command;
using Application.Events.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            var createdEvent = await _mediator.Send(command);

            if (createdEvent == null)
            {
                return BadRequest(new { Message = "No se pudo crear el evento." });
            }

            return CreatedAtAction(nameof(GetBookingById), new { id = createdEvent.EventId }, createdEvent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateEventCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id, CancellationToken ct)
        {
            try
            {
                await _mediator.Send(new DeleteEventCommand(id), ct);
                return Ok(new { message = "Evento eliminado exitosamente." });
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is KeyNotFoundException)
            {
                return Problem(ex.Message, statusCode: ex is InvalidOperationException ? 400 : 404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListBooking()
        {
            try
            {
                var events = await _mediator.Send(new GetEventListQuery());
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la lista de eventos.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var events = await _mediator.Send(new GetEventByIdQuery(id));

                if (events == null)
                {
                    return NotFound(new { Message = $"El evento con ID {id} no encontrado." });
                }

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la reservacion.", Details = ex.Message });
            }
        }

    }
}
