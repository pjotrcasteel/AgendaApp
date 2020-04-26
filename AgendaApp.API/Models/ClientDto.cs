using System;

namespace AgendaApp.API.Models
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AppointmentTodayCount { get; set; }
    }
}
