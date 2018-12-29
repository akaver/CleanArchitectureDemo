using DAL.Core;
using DAL.Repositories;
using Domain;

namespace DAL.EF.Repositories
{
    public class ContactTypeEFRepository: EFRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeEFRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}