﻿using AgendaApp.API.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Models
{
    /// <summary>
    /// This Class AppointmentDto is used to transfer return Data 
    /// </summary>
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; }
        public bool IsFullDay { get; set; }
        public string Type { get; set; }
        public AppointmentStatus Status { get; set; }

        public Guid ClientId { get; set; }
    }
}
