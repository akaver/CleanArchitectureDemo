using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;

namespace DAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();

        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);

        void Remove(params object[] id);
    }
}