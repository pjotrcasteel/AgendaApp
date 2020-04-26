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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AgendaDbContext dbContext;

        public GenericRepository(AgendaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal AgendaDbContext GetContext()
            => dbContext;

        public async Task<IEnumerable<Guid>> Create(IEnumerable<TEntity> entity)
        {
            dbContext.Set<TEntity>().AddRange(entity);
            await dbContext.SaveAsync();
            return entity.Select(e => e.Id);
        }

        public async Task<Guid> Create(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            await dbContext.SaveAsync();
            return entity.Id;
        }

        public IQueryable<TEntity> Get()      
            => dbContext.Set<TEntity>()
                .AsNoTracking();
        

        public async Task<TEntity> Get(Guid id)
            => await dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<TEntity>> Get(IEnumerable<Guid> ids)
            => await dbContext.Set<TEntity>()
                .Where(e => ids.Contains(e.Id))
                .AsNoTracking()
                .ToListAsync();


        public async Task<IEnumerable<TEntity>> Get(IQueryable<TEntity> query)
            => await query.ToListAsync();
        

        public async Task<(Guid, int)> Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return (entity.Id, await dbContext.SaveAsync());
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await Get(id);
            dbContext.Set<TEntity>().Remove(entity);
            return (await dbContext.SaveAsync()) > 0;
        }
    }
}
