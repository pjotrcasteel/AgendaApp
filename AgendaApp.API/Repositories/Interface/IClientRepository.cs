using AgendaApp.API.Entities;
using AgendaApp.API.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Repositories.Interface
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<bool> ClientExists(Guid clientId);
        Task<IEnumerable<Client>> Get(ClientsResourceParameters clientsResourceParameters);
    }
}