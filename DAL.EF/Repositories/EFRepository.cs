using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext RepositoryDbContext;
        protected DbSet<TEntity> RepositoryDbSet;

        public EFRepository(IDataContext dataContext)
        {
            RepositoryDbContext = dataContext as DbContext ?? throw new ArgumentNullException(nameof(dataContext));
            RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> All() => RepositoryDbSet.ToList();


        public virtual async Task<IEnumerable<TEntity>> AllAsync() => await RepositoryDbSet.ToListAsync();

        public virtual TEntity Find(params object[] id) => RepositoryDbSet.Find(id);

        public virtual async Task<TEntity> FindAsync(params object[] id) => await RepositoryDbSet.FindAsync(id);

        public void Add(TEntity entity) => RepositoryDbSet.Add(entity);

        public virtual async Task AddAsync(TEntity entity) => await RepositoryDbSet.AddAsync(entity);

        public TEntity Update(TEntity entity) => RepositoryDbSet.Update(entity).Entity;

        public void Remove(TEntity entity) => RepositoryDbSet.Remove(entity);

        public void Remove(params object[] id)
        {
            var entity = RepositoryDbSet.Find(id);
            Remove(entity);
        }
        
    }
}