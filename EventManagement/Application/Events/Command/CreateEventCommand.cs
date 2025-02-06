using Application.DTO;
using Application.Services.Interfaces;
using MediatR;
using Domain.Entities;


namespace Application.Events.Command
{
    public readonly record struct CreateEventCommand(
        string Name,
        string Description,
        DateTime DateTime,
        string Location,
        int MaxCapacity,
        int CreatedByUserId) : IRequest<EventDTO>;

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDTO>
    {
        
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventDTO> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {

            var events = new Event
            {
                Name = request.Name,
                Description = request.Description,
                DateTime = request.DateTime,
                Location = request.Location,
                MaxCapacity = request.MaxCapacity,
                CreatedByUserId = request.CreatedByUserId
            };

            await _eventRepository.Add(events);

            return new EventDTO();
        }
    }
}
