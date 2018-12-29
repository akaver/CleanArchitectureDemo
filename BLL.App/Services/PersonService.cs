using BLL.Services;
using DAL;
using DAL.Core;
using Domain;

namespace BLL.App.Services
{
    public class PersonService : EntityService<Person>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : base(uow)
        {
        }
    }
}