using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using DAL.EF.Repositories;
using DAL.Repositories;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly IDataContext _dataContext;
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();

        public AppUnitOfWork(IDataContext dataContext)
        {
            if (!(dataContext is DbContext))
            {
                throw new ArgumentException(nameof(dataContext) +
                                            " has to be derived from EF DbContext in this UOW implementation");
            }

            _dataContext = dataContext;
        }

        
        public int SaveChanges() => ((DbContext) _dataContext).SaveChanges();

        public Task<int> SaveChangesAsync() => ((DbContext) _dataContext).SaveChangesAsync();


        public IPersonRepository Persons =>
            GetOrCreateRepository<IPersonRepository>((dataContext) => new PersonEFRepository(dataContext));

        public IContactRepository Contacts =>
            GetOrCreateRepository<IContactRepository>((dataContext) => new ContactEFRepository(dataContext));

        public IContactTypeRepository ContactTypes =>
            GetOrCreateRepository<IContactTypeRepository>((dataContext) => new ContactTypeEFRepository(dataContext));


        public IRepository<TEntity> BaseRepository<TEntity>() where TEntity : BaseEntity =>
            GetOrCreateRepository<IRepository<TEntity>>((dataContext) => new EFRepository<TEntity>(dataContext));

      
        private TRepository GetOrCreateRepository<TRepository>(Func<IDataContext, object> factoryMethod)
        {
            _repositoryCache.TryGetValue(key: typeof(TRepository), value: out var repoObj);
            if (repoObj != null)
            {
                return (TRepository) repoObj;
            }

            repoObj = factoryMethod(_dataContext);
            _repositoryCache[key: typeof(TRepository)] = repoObj;

            return (TRepository) repoObj;
        }
    }
}