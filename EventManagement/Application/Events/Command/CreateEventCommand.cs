using Application.DTO;
using Application.Services.Interfaces;
using MediatR;

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
        
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {

            var events = new EventDTO
            {
                Name = request.Name,
                Description = request.Description,
                DateTime = request.DateTime,
                Location = request.Location,
                MaxCapacity = request.MaxCapacity,
                CreatedByUserId = request.CreatedByUserId
            };

            await _eventRepository.Add(events);

            return $"Evento creado con éxito: {events.Name}, ID: {events.EventId}";
        }
    }
}
