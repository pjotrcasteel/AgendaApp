using AgendaApp.API.Context;
using AgendaApp.API.Entities;
using AgendaApp.API.Repositories.Interface;
using AgendaApp.API.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.API.Repositories.Implementation
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AgendaDbContext context):base(context)
        {
        }

        public async Task<bool> ClientExists(Guid clientId)
            => (await Get(clientId)) != null;

        public async Task<IEnumerable<Client>> Get(ClientsResourceParameters clientsResourceParameters)
            => clientsResourceParameters.HasAppointmentToday.HasValue || !string.IsNullOrWhiteSpace(clientsResourceParameters.SearchQuery)
                ? await Get(CreateFilterSearchQuery(clientsResourceParameters))
                : await Get().Include(c => c.Appointments).ToListAsync();         


        private IQueryable<Client> CreateFilterSearchQuery(ClientsResourceParameters clientsResourceParameters)
        {
            var query = GetContext().Clients as IQueryable<Client>;

            if (clientsResourceParameters.HasAppointmentToday.HasValue)
                query = query.Where(c => c.Appointments.Any(a => a.Start.Date == DateTime.Today));

            if (!string.IsNullOrWhiteSpace(clientsResourceParameters.SearchQuery))
            {
                var searchQuery = clientsResourceParameters.SearchQuery;
                query = query.Where(c => c.FirstName.Contains(searchQuery) || c.LastName.Contains(searchQuery));
            }
            
            return query.Include(q => q.Appointments);
        }
    }
}
