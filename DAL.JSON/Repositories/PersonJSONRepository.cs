using System.Collections.Generic;
using DAL.Core;
using DAL.Repositories;
using Domain;

namespace DAL.JSON.Repositories
{
    public class PersonJSONRepository : JSONRepository<Person>, IPersonRepository 
    {
        public PersonJSONRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}