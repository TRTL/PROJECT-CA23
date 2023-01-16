using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Models;
using PROJECT_CA23.Services.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROJECT_CA23.Models.Dto.UserDtos;
using PROJECT_CA23.Models.Dto.AddressDto;

namespace PROJECT_CA23.Services.Adapters_Tests
{
    [TestClass()]
    public class UserAdapter_Tests
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

        [TestMethod()]
        public void Bind_ShouldReturnUser_WhenProvidedWithUserAndUpdateUserDto()
        {
            // Arrange
            var fake_User = _fake_User;

            var fake_UpdateUserDto = new UpdateUserDto()
            {
                FirstName = "Antanas",
                LastName = "Antanauskas"
            };

            var expected = new User()
            {
                UserId = 1,
                Username = "Jonas3000",
                FirstName = "Antanas",
                LastName = "Antanauskas",
                Role = ERole.user,
                PasswordHash = new byte[8],
                PasswordSalt = new byte[8],
                Created = fake_User.Created,
                Updated = fake_User.Updated
            };

            // Act
            var sut = new UserAdapter();
            User actual = sut.Bind(fake_User, fake_UpdateUserDto);

            // Assert
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }


        [TestMethod()]
        public void Bind_ShouldReturnUserDto_WhenProvidedWithUser()
        {
            // Arrange

            var fake_User = _fake_User;

            var expected = new UserDto
            {
                UserId = fake_User.UserId,
                FirstName = fake_User.FirstName,
                LastName = fake_User.LastName,
                Username = fake_User.Username,
                Role = fake_User.Role.ToString(),
                Created = fake_User.Created
            };

            // Act
            var sut = new UserAdapter();
            UserDto actual = sut.Bind(fake_User);

            // Assert
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Created, actual.Created);

        }
    }
}