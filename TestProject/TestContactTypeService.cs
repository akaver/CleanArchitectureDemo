using BLL.App.Services;
using DAL.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestProject
{
    public class TestContactTypeService
    {
        private ContactTypeService _contactTypeService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test_Database")
                .Options;

            var dbContext = new AppDbContext(options);
            var uow = new AppUnitOfWork(dbContext);

            _contactTypeService = new ContactTypeService(uow);
        }

        [Test]
        public void TestContactTypeCreate()
        {
            CreateContactType(1);

            var foundContactType = _contactTypeService.Find(1);
            Assert.IsNotNull(foundContactType);
        }

        [Test]
        public void TestContactTypeUpdate()
        {
            const int id = 2;
            const string editedValue = "value_changed";

            CreateContactType(id);

            var foundContactType = _contactTypeService.Find(id);
            foundContactType.Value = editedValue;

            _contactTypeService.Update(foundContactType);

            var foundContactTypeAfterEdit = _contactTypeService.Find(id);

            Assert.IsNotNull(foundContactTypeAfterEdit);
            Assert.AreEqual(editedValue, foundContactTypeAfterEdit.Value);
        }

        [Test]
        public void TestContactTypeDelete()
        {
            const int id = 3;

            CreateContactType(id);
            _contactTypeService.Remove(id);
            var foundContactTypeAfterRemove = _contactTypeService.Find(id);

            Assert.IsNull(foundContactTypeAfterRemove);
        }

        private void CreateContactType(int id)
        {
            ContactType contactType = new ContactType
            {
                Id = id,
                Value = $"testvalue_{id}"
            };

            _contactTypeService.Add(contactType);
        }
    
    }
}
