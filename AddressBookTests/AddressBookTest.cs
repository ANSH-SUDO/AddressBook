using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using NUnit.Framework;
using RepositoryLayer.Context;
using RepositoryLayer.Service;

namespace AddressBookTests
{
    public class AddressBookTest
    {
        private AddressBookServiceBL _addressBL;
        private AddressBookServiceRL _addressRL;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AddressContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

            var context = new AddressContext(options);
            _addressRL = new AddressBookServiceRL(context);
            _addressBL = new AddressBookServiceBL(_addressRL);
        }

        [Test]
        public void AddAddress_ShouldReturnTrue_WhenAddressIsValid()
        {
            var address = new AddressBookEntryDTO
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "1234567890"
            };

            var result = _addressBL.AddContact(address);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddAddress_ShouldReturnFalse_WhenAddressIsInvalid()
        {
            var address = new AddressBookEntryDTO
            {
                Name = "", 
                Email = "invalid-email",
                Password = "123"
            };

            var result = _addressBL.AddContact(address);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetAllAddresses_ShouldReturnListOfAddresses()
        {
            var result = _addressBL.GetAllContacts();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count >= 0);
        }
    }
}
