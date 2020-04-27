using AgendaApp.API.Entities;
using AgendaApp.API.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Repositories.Interface
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByClientId(Guid clientId);
        Task<(Guid, AppointmentStatus)> CreateWithCheck(Guid clientId, Appointment appointmentEntity);
        Task<Appointment> Get(Guid clientId, Guid appointmentId);
        Task UpdateWithCheck(Appointment appointment);
        Task<bool> Delete(Guid clientId, Guid appointmentId);
    }
}
