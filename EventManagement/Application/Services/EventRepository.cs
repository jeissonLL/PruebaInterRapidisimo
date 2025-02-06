using Application.Services.Interfaces;
using Infraestructure.Persistence;
using Domain.Entities;

namespace Application.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> Add(Event CreateEvent)
        {
            try
            {
                _context.Events.Add(CreateEvent);
                await _context.SaveChangesAsync();
                return CreateEvent;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el evento", ex);
            }
        }
    }
}
