using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EventUser
    {
        [Key]
        public int EventUserId { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; } = null!;

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public bool IsAttending { get; set; } = false;

        public DateTime RegistrationDate { get; set; }
    }
}
