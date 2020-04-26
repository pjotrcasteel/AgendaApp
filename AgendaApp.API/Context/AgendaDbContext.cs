using AgendaApp.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgendaApp.API.Context
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        internal async Task<int> SaveAsync()      
            => await SaveChangesAsync();
        
    }
}
