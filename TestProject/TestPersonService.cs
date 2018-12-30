using System.Collections.Generic;
using BLL.App.Services;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    public class TestPersonService
    {
        private PersonService _personService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test_Database")
                .Options;

            var dbContext = new AppDbContext(options);
            var uow = new AppUnitOfWork(dbContext);

            _personService = new PersonService(uow);
        }

        [Test]
        public void TestPersonCreate()
        {
            CreatePerson(1);

            var foundPerson = _personService.Find(1);
            Assert.IsNotNull(foundPerson);
        }

        [Test]
        public void TestPersonUpdate()
        {
            const int id = 2;
            const string editedFirstName = "firstname_changed";
            const string editedLastName = "lastname_changed";

            CreatePerson(id);

            var foundPerson = _personService.Find(id);
            foundPerson.FirstName = editedFirstName;
            foundPerson.LastName = editedLastName;

            _personService.Update(foundPerson);

            var foundPersonAfterEdit = _personService.Find(id);

            Assert.IsNotNull(foundPersonAfterEdit);
            Assert.AreEqual(editedFirstName, foundPersonAfterEdit.FirstName);
            Assert.AreEqual(editedLastName, foundPersonAfterEdit.LastName);
        }

        [Test]
        public void TestPersonDelete()
        {
            const int id = 3;

            CreatePerson(id);
            _personService.Remove(id);
            var foundPersonAfterRemove = _personService.Find(id);

            Assert.IsNull(foundPersonAfterRemove);
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
    }
}