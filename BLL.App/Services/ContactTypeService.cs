using BLL.Services;
using DAL;
using DAL.Core;
using Domain;

namespace BLL.App.Services
{
    public class ContactTypeService: EntityService<ContactType>, IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork uow) : base(uow)
        {
        }
    }
}