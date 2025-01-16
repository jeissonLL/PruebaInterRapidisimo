using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.DTO;

namespace Application.Events.Query
{
    public readonly record struct GetEventListQuery : IRequest<List<EventDTO>>;
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, List<EventDTO>>
    {
        private readonly ApplicationDbContext _context;

        public GetEventListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventDTO>> Handle(GetEventListQuery request, CancellationToken ct)
        {
            var events = await _context.Events
                .Select(e => new EventDTO
                {
                    EventId = e.EventId,
                    Name = e.Name,
                    Description = e.Description,
                    DateTime = e.DateTime,
                    Location = e.Location,
                    MaxCapacity = e.MaxCapacity
                })
                .ToListAsync(ct);

            return events;
        }
    }
}
