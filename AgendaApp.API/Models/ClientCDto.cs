using AgendaApp.API.ValidationAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Models
{
    public class ClientCDto
    {
        public ClientCDto()
        {
            Appointments = new HashSet<AppointmentCDto>();
        }

        [Required]
        [MaxLength(40, ErrorMessage = "First name has a max. of 40 chars")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Last name has a max. of 40 chars")]
        public string LastName { get; set; }

        [NoAppointmentOverlapAllowed]
        public ICollection<AppointmentCDto> Appointments { get; set; }
    }
}
