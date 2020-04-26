using AgendaApp.API.Context;
using AgendaApp.API.Entities;
using AgendaApp.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.API.Repositories.Implementation
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AgendaDbContext context) : base(context)
        {
        }

        public async Task<Appointment> Get(Guid clientId, Guid appointmentId)
        {
            var appointment = GetContext().Appointments.Where(a => a.ClientId == clientId && a.Id == appointmentId).FirstOrDefault();
            return await Task.FromResult(appointment);
        }

        public async Task<IEnumerable<Appointment>> GetByClientId(Guid clientId)
        {
            var appointments = GetContext().Appointments.Where(a => a.ClientId == clientId).ToList();
            return await Task.FromResult(appointments);
        }

        public async Task<bool> Delete(Guid clientId, Guid AppointmentId)
        {
            // Check if appointment is of client
            var appointment = GetContext().Appointments.Where(a => a.ClientId == clientId).FirstOrDefault();
            if(appointment == null)
            {
                return false;
            }
            GetContext().Entry(appointment).State = EntityState.Detached;
            var deleted = await Delete(AppointmentId);
            return await Task.FromResult(deleted);
        }
    }
}
