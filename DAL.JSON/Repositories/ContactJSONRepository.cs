using System.Collections.Generic;
using DAL.Core;
using DAL.Repositories;
using Domain;

namespace DAL.JSON.Repositories
{
    public class ContactJSONRepository : JSONRepository<Contact>, IContactRepository
    {
        public ContactJSONRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}