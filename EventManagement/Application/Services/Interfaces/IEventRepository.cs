using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> Add(Event CreateEvent);
    }
}
