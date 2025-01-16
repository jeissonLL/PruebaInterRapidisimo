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
        public string Name { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}

