using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Events.Command
{
    public readonly record struct UpdateEventCommand(
            int EventId,
            int MaxCapacity,
            DateTime DateTime,
            string Location) : IRequest<string>;

    public class UpdateEventCommandHandle : IRequestHandler<UpdateEventCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public UpdateEventCommandHandle(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateEventCommand request, CancellationToken ct)
        {
            var updateBooking = await _context.Events
                .FirstOrDefaultAsync(c => c.EventId == request.EventId, ct);

            if (updateBooking == null)
            {
                throw new KeyNotFoundException($"El evento con ID {request.EventId} no encontrado.");
            }

            updateBooking.MaxCapacity = request.MaxCapacity;
            updateBooking.DateTime = request.DateTime;
            updateBooking.Location= request.Location;

            await _context.SaveChangesAsync(ct);

            return updateBooking.EventId.ToString();
        }
    }

}
