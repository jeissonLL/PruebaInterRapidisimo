using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Events.Command
{
    public readonly record struct DeleteEventCommand(int EventId) : IRequest<string>;

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public DeleteEventCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteEventCommand request, CancellationToken ct)
        {
            // Buscar el evento
            var events = await _context.Events
                .FirstOrDefaultAsync(c => c.EventId == request.EventId, ct);

            if (events == null)
            {
                throw new KeyNotFoundException($"El evento con ID {request.EventId} no fue encontrado.");
            }

            // Verificar si tiene asistentes inscritos
            var hasAttendees = await _context.EventUsers
                .AnyAsync(eu => eu.EventId == request.EventId, ct);

            if (hasAttendees)
            {
                throw new InvalidOperationException("El evento no puede ser eliminado porque tiene asistentes inscritos.");
            }

            // Eliminar el evento si no tiene asistentes
            _context.Events.Remove(events);
            await _context.SaveChangesAsync(ct);

            return $"Evento con ID {events.EventId} eliminado exitosamente.";
        }
    }
}
