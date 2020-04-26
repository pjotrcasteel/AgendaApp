using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Models
{
    /// <summary>
    /// This Class AppointmentU"pdate"Dto is used to transfer incoming Update Data 
    /// </summary>
    public class AppointmentUDto
    {
        [Required]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; }
        public bool IsFullDay { get; set; }
        public string Type { get; set; }
    }
}
