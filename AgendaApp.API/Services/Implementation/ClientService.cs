using AgendaApp.API.Entities;
using AgendaApp.API.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApp.API.Repositories.Interface;
using AgendaApp.API.Resources;

namespace AgendaApp.API.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository repository;
        public ClientService(IClientRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Guid>> Create(IEnumerable<Client> clientEntities)
            => await repository.Create(clientEntities);

        public async Task<Guid> Create(Client clientEntity)
            => await repository.Create(clientEntity);

        public async Task<IEnumerable<Client>> GetClients(ClientsResourceParameters clientsResourceParameters)
            => await repository.Get(clientsResourceParameters);

        public async Task<IEnumerable<Client>> GetClients(IEnumerable<Guid> clientIds)
            => await repository.Get(clientIds);

        public async Task<Client> GetClient(Guid clientId)
        {
            return await repository.Get(clientId);
        }
    }
}
