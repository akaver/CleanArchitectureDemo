using System.Collections.Generic;
using DAL.Core;
using DAL.Repositories;
using Domain;

namespace DAL.JSON.Repositories
{
    public class ContactTypeJSONRepository: JSONRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeJSONRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}