using System;
namespace Application.DTO
{
    public class EventDTO
    {
        public int EventId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public string Location { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
