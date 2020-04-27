using AgendaApp.API.Context;
using AgendaApp.API.Entities;
using AgendaApp.API.Repositories.Interface;
using AgendaApp.API.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AgendaApp.API.Repositories.Implementation
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AgendaDbContext context) : base(context)
        {
        }

        public async Task<(Guid, AppointmentStatus)> CreateWithCheck(Guid clientId, Appointment appointmentEntity)
        {
            var appointment = appointmentEntity;
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                appointment.ClientId = clientId;
                var appointments = GetContext().Appointments
                    .Where(a => a.ClientId == clientId && a.Start.Date == appointment.Start.Date);

                if (appointments.Any())
                {
                    appointment.Status = HasDublicateAppointments(appointment, appointments)
                        ? AppointmentStatus.Overlap.ToString()
                        : AppointmentStatus.Ok.ToString();
                }
                await Create(appointment);

                transactionScope.Complete();
                transactionScope.Dispose();
            }
            Enum.TryParse(appointment.Status, out AppointmentStatus status);
            return (appointment.Id, status);
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

        public async Task UpdateWithCheck(Appointment appointment)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var appointments = GetContext().Appointments
                    .Where(a => a.ClientId == appointment.ClientId && a.Start.Date == appointment.Start.Date && a.Id != appointment.Id);

                if (appointments.Any())
                {
                    appointment.Status = HasDublicateAppointments(appointment, appointments)
                        ? AppointmentStatus.Overlap.ToString()
                        : AppointmentStatus.Ok.ToString();
                }
                await Update(appointment);

                transactionScope.Complete();
                transactionScope.Dispose();
            }
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

        private bool HasDublicateAppointments(Appointment appointment, IQueryable<Appointment> appointments)
        {
            if (appointments.Any(a => a.IsFullDay && a.Start.Date == appointment.Start.Date))
                return true;

            var appointmentsList = appointments
                .Select(a => new ReducedAppointment 
                { 
                    IsFullDay = a.IsFullDay, 
                    Start = a.Start, 
                    End = a.End 
                })
                .ToList();
            
            appointmentsList.Add(new ReducedAppointment { IsFullDay = appointment.IsFullDay, Start = appointment.Start, End = appointment.End });
            appointmentsList = appointmentsList.OrderBy(a => a.Start).ToList();

            var hasDublicateAppointments = appointmentsList
                .Zip(appointmentsList.Skip(1), (a, b) => a.Start > b.Start || a.End > b.Start)
                .Any(a => a == true);

            return hasDublicateAppointments;
        }

        internal class ReducedAppointment
        {
            internal bool IsFullDay { get; set; }
            internal DateTime Start { get; set; }
            internal DateTime End { get; set; }
        }
    }
}
