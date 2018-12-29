using System;
using System.Collections.Generic;
using BLL.App.Services;
using BLL.Services;
using DAL;
using DAL.Core;

namespace BLL.App
{
    public class AppBLL : IAppBLL
    {
        private readonly IAppUnitOfWork _uow;
        private readonly Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();


        public AppBLL(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        public IPersonService Persons => GetOrCreateService<IPersonService>((uow) => new PersonService(uow));
        public IContactService Contacts => GetOrCreateService<IContactService>((uow) => new ContactService(uow));
        public IContactTypeService ContactTypes => GetOrCreateService<IContactTypeService>((uow) => new ContactTypeService(uow));
        
        private TService GetOrCreateService<TService>(Func<IAppUnitOfWork, object> factoryMethod)
        {
            _serviceCache.TryGetValue(key: typeof(TService), value: out var serviceObj);
            if (serviceObj != null)
            {
                return (TService) serviceObj;
            }

            serviceObj = factoryMethod(_uow);
            _serviceCache[key: typeof(TService)] = serviceObj;

            return (TService) serviceObj;
        }
        
    }
}