using AgendaApp.API.Entities;
using AgendaApp.API.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Services.Interface
{
    public interface IAppointmentService
    {
        Task<bool> ClientExists(Guid clientId);
        Task<IEnumerable<Appointment>> GetAppointments(Guid clientId);
        Task<Appointment> GetAppointment(Guid appointmentId);
        Task<Appointment> GetAppointment(Guid clientId, Guid appointmentId);
        Task<(Guid, AppointmentStatus)> Create(Guid clientId, Appointment appointmentEntity);
        Task Update(Appointment appointmentEntity);
        Task<bool> Delete(Guid ClientId, Guid AppointmentId);
    }
}
