using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Services.Adapters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_CA23.Services.Adapters_Tests
{
    [TestClass()]
    public class UserMediaAdapter_Tests
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

        private readonly List<Genre> _fake_Genres = new List<Genre>()
        {
            new Genre
            {
                GenreId= 1,
                Name = "Sci-Fi"
            },
            new Genre
            {
                GenreId= 2,
                Name = "Action"
            }
        };

        private readonly Media _fake_Media = new Media()
        {
            MediaId = 1,
            Title = "Blade Runner",
            Year = "1982",
            Runtime = "117 min",
            Director = "Ridley Scott",
            Writer = "Hampton Fancher, David Webb Peoples, Philip K. Dick",
            Actors = "Harrison Ford, Rutger Hauer, Sean Young",
            Plot = "A blade runner must pursue and terminate four replicants who stole a ship in space and have returned to Earth to find their creator.",
            Language = "English, German, Cantonese, Japanese, Hungarian, Arabic, Korean",
            Country = "United States",
            Poster = "https://m.media-amazon.com/images/M/MV5BNzQzMzJhZTEtOWM4NS00MTdhLTg0YjgtMjM4MDRkZjUwZDBlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg",
            imdbRating = 8.1,
            imdbVotes = 771646,
            imdbId = "tt0083658",
            Type = "movie"
        };

        private readonly UserMedia _fake_UserMedia = new UserMedia()
        {
            UserMediaId = 1,
            Note = "test text",
            UserMediaStatus = EUserMediaStatus.Wishlist
        };



        [TestMethod()]
        public void Bind_ShouldReturnUserMediaDto_WhenProvidedWithUserMedia()
        {
            // Arrange
            var fake_User = _fake_User;

            var fake_Media = _fake_Media;
            fake_Media.Genres = _fake_Genres;

            var fake_UserMedia = _fake_UserMedia;
            fake_UserMedia.UserId = fake_User.UserId;
            fake_UserMedia.User = fake_User;
            fake_UserMedia.MediaId = fake_Media.MediaId;
            fake_UserMedia.Media = fake_Media;

            var expected = new UserMediaDto()
            {
                UserMediaId = 1,
                UserId = 1,
                MediaId = 1,
                Type = "movie",
                Title = "Blade Runner",
                Year = "1982",
                imdbId = "tt0083658",
            };

            // Act
            var sut = new UserMediaAdapter();
            UserMediaDto actual = sut.Bind(fake_UserMedia);

            // Assert
            Assert.AreEqual(expected.UserMediaId, actual.UserMediaId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.MediaId, actual.MediaId);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Year, actual.Year);
            Assert.AreEqual(expected.imdbId, actual.imdbId);
        }



        [TestMethod()]
        public void Bind_ShouldReturnUserMedia_WhenProvidedWithUserAndMedia()
        {
            // Arrange
            var fake_User = _fake_User;
            var fake_Media = _fake_Media;
            fake_Media.Genres = _fake_Genres;

            var expected = new UserMedia()
            {
                UserId= fake_User.UserId,
                User = fake_User,
                MediaId = fake_Media.MediaId,
                Media = fake_Media,
                Note = null,
                ReviewId = null,
                Review = null
            };

            // Act
            var sut = new UserMediaAdapter();
            UserMedia actual = sut.Bind(fake_User, fake_Media);

            // Assert
            Assert.AreEqual(expected.UserMediaId, actual.UserMediaId);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.MediaId, actual.MediaId);
        }

        [TestMethod()]
        public void Bind_ShouldReturnUserMedia_WhenProvidedWithUserMediaAndUpdateUserMediaRequest()
        {
            // Arrange
            var fake_User = _fake_User;

            var fake_Media = _fake_Media;
            fake_Media.Genres = _fake_Genres;

            var fake_UserMedia = _fake_UserMedia;
            fake_UserMedia.UserId = fake_User.UserId;
            fake_UserMedia.User = fake_User;
            fake_UserMedia.MediaId = fake_Media.MediaId;
            fake_UserMedia.Media = fake_Media;

            var fake_UpdateUserMediaRequest = new UpdateUserMediaRequest()
            {
                UserMediaId = fake_UserMedia.UserMediaId,
                UserMediaStatus = EUserMediaStatus.Finished.ToString(),
                UserRating = EUserRating.FiveStars.ToString(),
                ReviewText = "test text",
                Note = "test note"
            };

            var expected = new UserMedia()
            {
                UserId= fake_User.UserId,
                User = fake_User,
                MediaId = fake_Media.MediaId,
                Media = fake_Media,
                UserMediaStatus = EUserMediaStatus.Finished,
                Note = "test note",
                ReviewId = null,
                Review = null
            };

            // Act
            var sut = new UserMediaAdapter();
            UserMedia actual = sut.Bind(fake_UserMedia, fake_UpdateUserMediaRequest);

            // Assert
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.User, actual.User);
            Assert.AreEqual(expected.MediaId, actual.MediaId);
            Assert.AreEqual(expected.Media, actual.Media);
            Assert.AreEqual(expected.UserMediaStatus, actual.UserMediaStatus);
            Assert.AreEqual(expected.Note, actual.Note);

        }

    }
}