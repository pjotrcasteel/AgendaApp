using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApp.API.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; }
        public bool IsFullDay { get; set; }
        public string Type { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
    }
}