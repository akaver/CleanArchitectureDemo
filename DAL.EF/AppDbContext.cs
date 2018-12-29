using DAL.Core;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class AppDbContext : DbContext, IDataContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some data to db
            modelBuilder.Entity<Person>().HasData(
                new Person {Id = 1, FirstName = "Anu", LastName = "Kuusmaa"},
                new Person {Id = 2, FirstName = "Andres", LastName = "Käver"});

            modelBuilder.Entity<ContactType>().HasData(
                new ContactType {Id = 1, Value = "Skype"},
                new ContactType {Id = 2, Value = "E-Mail"});

            modelBuilder.Entity<Contact>().HasData(
                new Contact {Id = 1, PersonId = 1, ContactTypeId = 1, Value = "anuskype"},
                new Contact {Id = 2, PersonId = 1, ContactTypeId = 2, Value = "anu.kuusmaa@ttu.ee"},
                new Contact {Id = 3, PersonId = 2, ContactTypeId = 1, Value = "akaver"},
                new Contact {Id = 4, PersonId = 2, ContactTypeId = 2, Value = "andres.kaver@ttu.ee"});
        }
    }
}