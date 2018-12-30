using System.Collections.Generic;
using BLL.App.Services;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestProject
{
    public class TestContactService
    {
        private ContactService _contactService;
        private ContactTypeService _contactTypeService; 
        private PersonService _personService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test_Database")
                .Options;

            var dbContext = new AppDbContext(options);
            var uow = new AppUnitOfWork(dbContext);

            _contactService = new ContactService(uow);
            _contactTypeService = new ContactTypeService(uow);
            _personService = new PersonService(uow);

            CreatePerson(10);
            CreatePerson(20);
            CreateContactType(100);
            CreateContactType(200);
        }


        [Test]
        public void TestContactCreate()
        {
            CreateContact(1,10,100);

            var foundContact = _contactService.Find(1);
            Assert.IsNotNull(foundContact);
            Assert.AreEqual(100, foundContact.ContactTypeId);
            Assert.AreEqual(10, foundContact.PersonId);
        }

        [Test]
        public void TestContactUpdate()
        {
            const int id = 2;
            const string editedValue = "value_changed";
            const int editedPersonId = 10;
            const int editedContactTypeId = 100;

            CreateContact(id, 20, 200);

            var foundContact = _contactService.Find(id);
            foundContact.Value = editedValue;
            foundContact.PersonId = editedPersonId;
            foundContact.ContactTypeId = editedContactTypeId;

            _contactService.Update(foundContact);

            var foundContactAfterEdit = _contactService.Find(id);

            Assert.IsNotNull(foundContactAfterEdit);
            Assert.AreEqual(editedValue, foundContactAfterEdit.Value);
            Assert.AreEqual(editedPersonId, foundContactAfterEdit.PersonId);
            Assert.AreEqual(editedContactTypeId, foundContactAfterEdit.ContactTypeId);
        }

        [Test]
        public void TestContactDelete()
        {
            const int id = 3;

            CreateContact(id,10,100);
            _contactService.Remove(id);
            var foundContactAfterRemove = _contactService.Find(id);

            Assert.IsNull(foundContactAfterRemove);
        }

        private void CreatePerson(int id)
        {
            var person = new Person
            {
                FirstName = $"first_{id}",
                LastName = $"last_{id}",
                Id = id,
                Contacts = new List<Contact>()
            };

            _personService.Add(person);
        }

        private void CreateContactType(int id)
        {
            var contactType = new ContactType { Id = id, Value = $"value_{id}" };
            _contactTypeService.Add(contactType);
        }

        private void CreateContact(int contactId, int personId, int contactTypeId)
        {
            var contact = new Contact
            {
                Id = contactId,
                PersonId = personId,
                ContactTypeId = contactTypeId,
                Value = "initial_value"
            };
            _contactService.Add(contact);
        }
    }
}
