using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Core.Services;
using DAL;
using DAL.Core;
using Domain.Core;

namespace BLL.App.Services
{
    public class EntityService<TEntity> : IService<TEntity> where TEntity : BaseEntity
    {
        protected IUnitOfWork UOW;

        public EntityService(IUnitOfWork uow)
        {
            UOW = uow;
        }

        public virtual IEnumerable<TEntity> All() => UOW.BaseRepository<TEntity>().All();

        public virtual async Task<IEnumerable<TEntity>> AllAsync() => await UOW.BaseRepository<TEntity>().AllAsync();

        public virtual TEntity Find(params object[] id) => UOW.BaseRepository<TEntity>().Find(id);

        public virtual async Task<TEntity> FindAsync(params object[] id) =>
            await UOW.BaseRepository<TEntity>().FindAsync(id);

        public virtual void Add(TEntity entity) => UOW.BaseRepository<TEntity>().Add(entity);

        public virtual async Task AddAsync(TEntity entity) => await UOW.BaseRepository<TEntity>().AddAsync(entity);

        public virtual TEntity Update(TEntity entity) => UOW.BaseRepository<TEntity>().Update(entity);

        public virtual void Remove(TEntity entity) => UOW.BaseRepository<TEntity>().Remove(entity);

        public virtual void Remove(params object[] id) => UOW.BaseRepository<TEntity>().Remove(id);
    }
}