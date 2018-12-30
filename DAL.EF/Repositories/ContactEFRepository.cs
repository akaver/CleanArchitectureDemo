using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class ContactEFRepository : EFRepository<Contact>, IContactRepository

    {
        public ContactEFRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public override IEnumerable<Contact> All()
        {
            return RepositoryDbSet.Include(a => a.Person).Include(b => b.ContactType).ToList();
        }

        public override async Task<IEnumerable<Contact>> AllAsync()
        {
            return await RepositoryDbSet.Include(a => a.Person).Include(b => b.ContactType).ToListAsync();
        }

        public override Contact Find(params object[] id)
        {
            var entity = base.Find(id);
            if (entity == null)
                return null;
            
            RepositoryDbContext.Entry(entity).Reference(a => a.Person).Load();
            RepositoryDbContext.Entry(entity).Reference(a => a.ContactType).Load();
            return entity;
        }

        public override async Task<Contact> FindAsync(params object[] id)
        {
            var entity = await base.FindAsync(id);
            if (entity == null)
                return null;
            
            RepositoryDbContext.Entry(entity).Reference(a => a.Person).Load();
            RepositoryDbContext.Entry(entity).Reference(a => a.ContactType).Load();
            return entity;
        }
    }
}