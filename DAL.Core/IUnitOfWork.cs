using System.Threading;
using System.Threading.Tasks;
using DAL.Core.Repositories;
using Domain.Core;

namespace DAL.Core
{
    public interface IUnitOfWork
    {
        // save changes to underlaying datastore (if supported)
        int SaveChanges();
        Task<int> SaveChangesAsync();

        IRepository<TEntity> BaseRepository<TEntity>() where TEntity : BaseEntity;
    }
}