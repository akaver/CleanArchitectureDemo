using DAL.Core;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class AppDbContext : DbContext, IDataContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
    }
}