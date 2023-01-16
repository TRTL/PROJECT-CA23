using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Services.Adapters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_CA23.Services.Adapters_Tests
{
    [TestClass()]
    public class AddressAdapter_Tests
    {
        private readonly User _fake_User = new User()
        {
            UserId = 1,
            Username = "Jonas3000",
            FirstName = "Jonas",
            LastName = "Jonaitis",
            Role = ERole.user,
            PasswordHash = new byte[8],
            PasswordSalt = new byte[8],
            Created = DateTime.Now,
            Updated = DateTime.Now
        };

        private readonly Address _fake_Address = new Address()
        {
            AddressId = 1,
            Country = "Lithuania",
            City = "Kaunas",
            AddressText = "Kauno g. 123A",
            PostCode = "LT-12345"
        };


        [TestMethod()]
        public void  Bind_ShouldReturnAddressDto_WhenProvidedWithAddress()
        {
            // Arrange
            var fake_User = _fake_User;

            var fake_Address = _fake_Address;
            fake_Address.UserId = fake_User.UserId;
            fake_Address.User = fake_User;

            var expected = new AddressDto()
            {
                AddressId = 1,
                UserId = 1,
                FirstName = "Jonas",
                LastName = "Jonaitis",
                Country = "Lithuania",
                City = "Kaunas",
                AddressText = "Kauno g. 123A",
                PostCode = "LT-12345"
            };

            // Act
            var sut = new AddressAdapter();
            AddressDto actual = sut.Bind(fake_Address);

            // Assert
            Assert.AreEqual(expected.AddressId, actual.AddressId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.AddressText, actual.AddressText);
            Assert.AreEqual(expected.PostCode, actual.PostCode);
        }


        [TestMethod()]
        public void Bind_ShouldReturnAddress_WhenProvidedWithAddAddressRequestAndUser()
        {
            // Arrange
            var fake_AddAddressRequest = new AddAddressRequest()
            {
                UserId = 1,
                Country = "Lithuania",
                City = "Kaunas",
                AddressText = "Kauno g. 123A",
                PostCode = "LT-12345"
            };

            var fake_User = _fake_User;

            var expected = _fake_Address;
            expected.UserId = fake_User.UserId;
            expected.User = fake_User;

            // Act
            var sut = new AddressAdapter();
            Address actual = sut.Bind(fake_AddAddressRequest, fake_User);

            // Assert
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.AddressText, actual.AddressText);
            Assert.AreEqual(expected.PostCode, actual.PostCode);
        }



        [TestMethod()]
        public void Bind_ShouldReturnAddress_WhenProvidedWithAddressAndUpdateAddressRequest()
        {
            // Arrange
            var fake_UpdateAddressRequest = new UpdateAddressRequest()
            {
                AddressId = 1,
                Country = "Lithuania",
                City = "Vilnius",
                AddressText = "Vilnius g. 999",
                PostCode = "LT-00001"
            };

            var fake_User = _fake_User;

            var fake_Address = _fake_Address;
            fake_Address.UserId = fake_User.UserId;
            fake_Address.User = fake_User;

            var expected = new Address()
            {
                AddressId = 1,
                UserId = fake_User.UserId,
                User = fake_User,
                Country = "Lithuania",
                City = "Vilnius",
                AddressText = "Vilnius g. 999",
                PostCode = "LT-00001"
            };

            // Act
            var sut = new AddressAdapter();
            Address actual = sut.Bind(fake_Address, fake_UpdateAddressRequest);

            // Assert
            Assert.AreEqual(expected.AddressId, actual.AddressId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.AddressText, actual.AddressText);
            Assert.AreEqual(expected.PostCode, actual.PostCode);
        }
    }
}