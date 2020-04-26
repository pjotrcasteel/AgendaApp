using AgendaApp.API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AgendaApp.API.ValidationAttributes
{
    public class NoAppointmentOverlapAllowed : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var client = (ClientCDto)validationContext.ObjectInstance;

            var endPrior = DateTime.MinValue;
            foreach (var appointment in client.Appointments.OrderBy(a => a.Start))
            {
                if (appointment.Start > (appointment.End ?? appointment.Start.AddHours(1)))
                    return new ValidationResult("Invalid | Startdate cannot be after enddate ", new[] { "ClientCDto" });
                if (appointment.Start < endPrior)
                    return new ValidationResult($"Overlap | Startdate: {appointment.Start} cannot be before enddate: {endPrior}", new[] { "ClientCDto" });
                endPrior = appointment.End ?? appointment.Start.AddHours(1);
            }

            return ValidationResult.Success;
        }     
    }
}
