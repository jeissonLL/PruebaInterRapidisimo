using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventUser
    {
        [Key]
        public int EventUserId { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event{ get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool IsAttending { get; set; } = false;

        public DateTime RegistrationDate { get; set; }
    }
}
