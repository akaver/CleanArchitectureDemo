using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using DAL.JSON.Repositories;
using DAL.Repositories;
using Domain;
using Domain.Core;
using Newtonsoft.Json;

namespace DAL.JSON
{
    public class AppJSONUnitOfWork : IAppUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();
        private readonly IDataContext _dataContext;

        public IPersonRepository Persons =>
            GetOrCreateRepository<IPersonRepository>((dataContext) => new PersonJSONRepository(dataContext));

        public IContactRepository Contacts =>
            GetOrCreateRepository<IContactRepository>((dataContext) => new ContactJSONRepository(dataContext));

        public IContactTypeRepository ContactTypes =>
            GetOrCreateRepository<IContactTypeRepository>((dataContext) => new ContactTypeJSONRepository(dataContext));

        public AppJSONUnitOfWork(IDataContext dataContext)
        {
            if (!(dataContext is AppJSONContext))
            {
                throw new ArgumentException(nameof(dataContext) +
                                            " has to be JSON based context in this UOW implementation");
            }

            _dataContext = dataContext;
        }

        public int SaveChanges() => ((AppJSONContext) _dataContext).SaveChanges();

        public async Task<int> SaveChangesAsync() => await ((AppJSONContext) _dataContext).SaveChangesAsync();

        public IRepository<TEntity> BaseRepository<TEntity>() where TEntity : BaseEntity =>
            GetOrCreateRepository<IRepository<TEntity>>((dataCache) => new JSONRepository<TEntity>(dataCache));


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