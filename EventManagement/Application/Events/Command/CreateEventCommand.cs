using Application.Users.Command;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Events.Command
{
    public readonly record struct CreateEventCommand(
        string Name,
        string Description,
        DateTime DateTime,
        string Location,
        int MaxCapacity,
        int CreatedByUserId) : IRequest<string>;

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public CreateEventCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            // Verificar si el usuario creador existe antes de crear el evento
            var creatorExists = await _context.Users.FindAsync(request.CreatedByUserId);
            if (creatorExists == null)
            {
                throw new ArgumentException("El usuario creador no existe.");
            }

            var events = new Event
            {
                Name = request.Name,
                Description = request.Description,
                DateTime = request.DateTime,
                Location = request.Location,
                MaxCapacity = request.MaxCapacity,
                CreatedByUserId = request.CreatedByUserId
            };

            _context.Add(events);
            await _context.SaveChangesAsync(cancellationToken);

            return $"Evento creado con éxito: {events.Name}, ID: {events.EventId}";
        }
    }
}
