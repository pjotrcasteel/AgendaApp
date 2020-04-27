using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaApp.API.Models.Extensions
{
    public static class AppointmentCDtoExtension
    {
        public static (bool valid, string message) AssertNoOverlap(this IEnumerable<AppointmentCDto> appointments)
        {
            var endPrior = DateTime.MinValue;
            foreach (var appointment in appointments.OrderBy(a => a.Start))
            {
                if (appointment.Start > (appointment.End ?? appointment.Start.AddHours(1)))
                    return (false, "Invalid | Startdate cannot be after enddate ");
                if (appointment.Start < endPrior)
                    return (false, $"Overlap | Startdate: {appointment.Start} cannot be before enddate: {endPrior}");
                endPrior = appointment.End ?? appointment.Start.AddHours(1);
            }
            return (true, null);
        }
    }
}
