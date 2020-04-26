using AgendaApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Repositories.Interface
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByClientId(Guid clientId);
        Task<Appointment> Get(Guid clientId, Guid appointmentId);
        Task<bool> Delete(Guid clientId, Guid appointmentId);
    }
}
