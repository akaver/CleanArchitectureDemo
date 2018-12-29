using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Services;
using DAL;
using DAL.Core;
using Domain;

namespace BLL.App.Services
{
    public class ContactService : EntityService<Contact>, IContactService
    {
        protected new IAppUnitOfWork UOW;

        public ContactService(IAppUnitOfWork uow) : base(uow)
        {
            UOW = uow;
        }

        public override IEnumerable<Contact> All() => UOW.Contacts.All();

        public override async Task<IEnumerable<Contact>> AllAsync() => await UOW.Contacts.AllAsync();

        public override Contact Find(params object[] id) => UOW.Contacts.Find(id);

        public override async Task<Contact> FindAsync(params object[] id) => await UOW.Contacts.FindAsync(id);
    }
}