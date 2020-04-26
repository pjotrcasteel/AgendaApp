using AgendaApp.API.Entities;
using AgendaApp.API.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Services.Interface
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClients(ClientsResourceParameters clientsResourceParameters);
        Task<IEnumerable<Client>> GetClients(IEnumerable<Guid> clientIds);
        Task<Client> GetClient(Guid clientId);
        Task<Guid> Create(Client clientEntity);
        Task<IEnumerable<Guid>> Create(IEnumerable<Client> clientEntities);
    }
}
