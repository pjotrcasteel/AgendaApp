using AgendaApp.API.Models;
using AgendaApp.API.Models.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.ValidationAttributes
{
    public class NoAppointmentOverlapAllowed : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(validationContext.ObjectInstance is ClientCDto client))
                return new ValidationResult("Unable to validate due to error in casting", new[] { "ClientCDto" });

            var (valid, message) = client.Appointments.AssertNoOverlap();
            return valid 
                ? ValidationResult.Success
                : new ValidationResult(message, new[] { "ClientCDto.ICollection<AppointmentCDto>" });
        }     
    }
}
