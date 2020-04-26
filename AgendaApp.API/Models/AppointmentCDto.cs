using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Models
{
    /// <summary>
    /// This Class AppointmentC"reate"Dto is used to transfer incoming Create Data 
    /// </summary>
    public class AppointmentCDto
    {
        [Required]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; }
        public bool IsFullDay { get; set; }
        public string Type { get; set; }
    }
}
