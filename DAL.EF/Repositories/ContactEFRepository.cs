using DAL.Core;
using DAL.Repositories;
using Domain;

namespace DAL.EF.Repositories
{
    public class ContactEFRepository: EFRepository<Contact>, IContactRepository

    {
        public ContactEFRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}