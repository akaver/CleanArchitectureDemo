using Domain;
using NUnit.Framework;

namespace TestProject
{
    public class TestPerson
    {
        [Test]
        public void TestPersonNameMethods()
        {
            var person = new Person {FirstName = "first", LastName = "last"};

            Assert.AreEqual("first last", person.FirstLastName);
            Assert.AreEqual("last first", person.LastFirstName);
        }
    }
}