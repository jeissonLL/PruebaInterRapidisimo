using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; } 

    }
}

