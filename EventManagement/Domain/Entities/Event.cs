using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        [ForeignKey("CreatedByUserId")]
        public User CreatedByUser { get; set; } = null!;

        // Clave foránea para identificar al creador del evento
        [ForeignKey("CreatedByUser")]
        public int CreatedByUserId { get; set; }

    }
}
