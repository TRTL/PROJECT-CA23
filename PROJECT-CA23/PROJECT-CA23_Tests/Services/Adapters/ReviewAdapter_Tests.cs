using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Models;
using PROJECT_CA23.Services.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROJECT_CA23.Models.Dto.AddressDto;

namespace PROJECT_CA23.Services.Adapters_Tests
{
    [TestClass()]
    public class ReviewAdapter_Tests
    {
        [TestMethod()]
        public void Bind_ShouldReturnReview_WhenProvidedWithUserAndUserMedia()
        {
            // Arrange
            var fake_User = new User()
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

            var fake_UserMedia = new UserMedia()
            {
                UserMediaId = 1,
                UserId = 1,
                User = fake_User,
                MediaId = 1,
                Media = new Media(),
                Note = "test text",
                UserMediaStatus = EUserMediaStatus.Wishlist
            };

            var expected = new Review()
            {
                ReviewId = 1,
                UserId = 1,
                User = fake_User,
                UserMediaId = 1,
                UserMedia = fake_UserMedia,
                MediaId = 1,
                Media  = new Media(),
                UserRating = null,
                ReviewText = null
            };

            // Act
            var sut = new ReviewAdapter();
            Review actual = sut.Bind(fake_User, fake_UserMedia);

            // Assert
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.User, actual.User);
            Assert.AreEqual(expected.UserMediaId, actual.UserMediaId);
            Assert.AreEqual(expected.UserMedia, actual.UserMedia);
            Assert.AreEqual(expected.MediaId, actual.MediaId);
        }
    }
}