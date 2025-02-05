using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class EventDTO
    {
        public int EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string Location { get; set; }

        public int MaxCapacity { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
