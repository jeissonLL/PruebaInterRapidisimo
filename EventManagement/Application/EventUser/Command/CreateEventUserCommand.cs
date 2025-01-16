using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EventUser.Command
{
    public readonly record struct CreateEventUserCommand(
        int EventId,
        int UserId,
        bool IsAttending,
        DateTime RegistrationDate) : IRequest<string>;

    public class CreateEventUserCommandHandler : IRequestHandler<CreateEventUserCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public CreateEventUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateEventUserCommand request, CancellationToken cancellationToken)
        {
            await ValidateEventExists(request.EventId, cancellationToken);
            await ValidateUserNotEventCreator(request.EventId, request.UserId, cancellationToken);
            await ValidateUserEventLimit(request.UserId, cancellationToken);
            await ValidateEventCapacity(request.EventId, cancellationToken);

            var eventUser = CreateEventUserEntity(request);
            _context.EventUsers.Add(eventUser);

            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                throw new Exception("No se pudo inscribir al usuario en el evento.");
            }

            return $"Usuario {request.UserId} inscrito correctamente en el evento {request.EventId}.";
        }

        /// <summary>
        /// Verifica si el evento existe en la base de datos.
        /// </summary>
        private async Task ValidateEventExists(int eventId, CancellationToken cancellationToken)
        {
            var eventExists = await _context.Events
                .AnyAsync(e => e.EventId == eventId, cancellationToken);

            if (!eventExists)
            {
                throw new InvalidOperationException("El evento especificado no existe.");
            }
        }

        /// <summary>
        /// Verifica que el usuario no sea el creador del evento.
        /// </summary>
        private async Task ValidateUserNotEventCreator(int eventId, int userId, CancellationToken cancellationToken)
        {
            var eventCreator = await _context.Events
                .Where(e => e.EventId == eventId)
                .Select(e => e.CreatedByUserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (eventCreator == userId)
            {
                throw new InvalidOperationException("Un usuario no puede inscribirse en un evento que él mismo ha creado.");
            }
        }

        /// <summary>
        /// Verifica si el usuario ha alcanzado el límite de inscripción de eventos.
        /// </summary>
        private async Task ValidateUserEventLimit(int userId, CancellationToken cancellationToken)
        {
            var userEventCount = await _context.EventUsers
                .CountAsync(eu => eu.UserId == userId, cancellationToken);

            if (userEventCount >= 3)
            {
                throw new InvalidOperationException("El usuario ya está inscrito en el máximo permitido de 3 eventos.");
            }
        }

        /// <summary>
        /// Verifica si el evento ha alcanzado su capacidad máxima.
        /// </summary>
        private async Task ValidateEventCapacity(int eventId, CancellationToken cancellationToken)
        {
            var currentRegistrations = await _context.EventUsers
                .CountAsync(eu => eu.EventId == eventId, cancellationToken);

            var maxCapacity = await _context.Events
                .Where(e => e.EventId == eventId)
                .Select(e => e.MaxCapacity)
                .FirstOrDefaultAsync(cancellationToken);

            if (currentRegistrations >= maxCapacity)
            {
                throw new InvalidOperationException("El evento ha alcanzado su capacidad máxima.");
            }
        }


        /// <summary>
        /// Crea una nueva entidad EventUser.
        /// </summary>
        private Domain.Entities.EventUser CreateEventUserEntity(CreateEventUserCommand request)
        {
            return new Domain.Entities.EventUser
            {
                EventId = request.EventId,
                UserId = request.UserId,
                IsAttending = request.IsAttending,
                RegistrationDate = request.RegistrationDate
            };
        }
    }
}
