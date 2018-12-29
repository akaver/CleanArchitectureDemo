using BLL.Services;
using DAL;
using DAL.Core;
using Domain;

namespace BLL.App.Services
{
    public class ContactService : EntityService<Contact>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(uow)
        {
        }
    }
}