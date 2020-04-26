using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Entities
{
    public class Client : BaseEntity
    {
        public Client() => Appointments = new HashSet<Appointment>();       

        [Required]
        [MaxLength(40, ErrorMessage = "First name has a max. of 40 chars")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Last name has a max. of 40 chars")]
        public string LastName { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        // Methodes
        public string GetName() 
            => $"{FirstName} {LastName}";
    }
}
