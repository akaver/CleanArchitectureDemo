using DAL.Core;
using DAL.Repositories;
using Domain;

namespace DAL.EF.Repositories
{
    public class PersonEFRepository : EFRepository<Person>, IPersonRepository 
    {
        public PersonEFRepository(IDataContext dataContext) : base(dataContext)
        {
        }
    }
}