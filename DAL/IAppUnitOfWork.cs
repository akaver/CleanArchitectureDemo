using DAL.Core;
using DAL.Repositories;

namespace DAL
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IPersonRepository Persons { get; }
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
    }
}