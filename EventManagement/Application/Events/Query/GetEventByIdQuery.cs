using Infraestructure.Persistence;
using MediatR;
using Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace Application.Events.Query
{
    public readonly record struct GetEventByIdQuery(int EventId) : IRequest<EventDTO>;
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDTO>
    {
        private readonly ApplicationDbContext _context;

        public GetEventByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EventDTO> Handle(GetEventByIdQuery request, CancellationToken ct)
        {
            var booking = await _context.Events
                .Where(c => c.EventId == request.EventId)
                .Select(c => new EventDTO
                {
                    Name = c.Name,
                    Description = c.Description,
                    DateTime = c.DateTime,
                    Location = c.Location,
                    MaxCapacity = c.MaxCapacity
                })
                .FirstOrDefaultAsync(ct);

            return booking!;
        }
    }
}
