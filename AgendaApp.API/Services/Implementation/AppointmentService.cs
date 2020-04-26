using AgendaApp.API.Entities;
using AgendaApp.API.Repositories.Interface;
using AgendaApp.API.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository repository;
        private readonly IClientRepository clientRepository;
        public AppointmentService(IAppointmentRepository repository, IClientRepository clientRepository)
        {
            this.repository = repository;
            this.clientRepository = clientRepository;
        }

        public async Task<Guid> Create(Guid clientId, Appointment appointmentEntity)
        {
            appointmentEntity.ClientId = clientId;
            return await repository.Create(appointmentEntity);
        }

        public async Task<bool> ClientExists(Guid clientId)
            => await clientRepository.ClientExists(clientId);

        public async Task<IEnumerable<Appointment>> GetAppointments(Guid clientId)
            => await repository.GetByClientId(clientId);

        public async Task<Appointment> GetAppointment(Guid appointmentId)
            => await repository.Get(appointmentId);

        public async Task<Appointment> GetAppointment(Guid clientId, Guid appointmentId)
            => await repository.Get(clientId, appointmentId);

        public async Task<(Guid, int)> Update(Appointment appointment)
            => await repository.Update(appointment);

        public async Task<bool> Delete(Guid ClientId, Guid AppointmentId)
            => await repository.Delete(ClientId, AppointmentId);
    }
}
