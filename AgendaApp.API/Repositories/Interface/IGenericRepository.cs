using AgendaApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.API.Repositories.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity 
    {
        Task<Guid> Create(TEntity entity);
        Task<IEnumerable<Guid>> Create(IEnumerable<TEntity> entity);
        IQueryable<TEntity> Get();
        Task<TEntity> Get(Guid id);
        Task<IEnumerable<TEntity>> Get(IQueryable<TEntity> entity);
        Task<IEnumerable<TEntity>> Get(IEnumerable<Guid> entities);
        Task<(Guid, int)> Update(TEntity entity);
        Task<bool> Delete(Guid id);
    }
}
