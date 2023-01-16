using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROJECT_CA23.Controllers;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.LoginDtos;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_CA23.Controllers_Tests
{
    [TestClass()]
    public class MediaController_Tests
    {
        private readonly Media _media_example = new Media()
        {
            MediaId = 1,
            Type = "movie",
            Title = "Blade Runner",
            Year = "2013",
            Runtime = "117 min",
            Director = "Ridley Scott",
            Writer = "Hampton Fancher, David Webb Peoples, Philip K. Dick",
            Actors = "Harrison Ford, Rutger Hauer, Sean Young",
            Plot = "A blade runner must pursue and terminate four replicants who stole a ship in space and have returned to Earth to find their creator.",
            Language = "English",
            Country = "United States",
            Poster = "https://m.media-amazon.com/images/M/MV5BNzQzMzJhZTEtOWM4NS00MTdhLTg0YjgtMjM4MDRkZjUwZDBlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg",
            imdbId = "tt0083658"
        };


        [TestMethod()]
        public async Task GetMediaById_ShouldReturnMedia_Successfully()
        {
            // Arrange
            var media_repo_mock = new Mock<IMediaRepository>();
            var logger_mock = new Mock<ILogger<MediaController>>();
            var media_adapter_mock = new Mock<IMediaAdapter>();
            var fake_media = _media_example;

            media_repo_mock.Setup(m => m.ExistAsync(It.IsAny<Expression<Func<Media, bool>>>())).ReturnsAsync(true);
            media_repo_mock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Media, bool>>>(), It.IsAny<ICollection<string>>(), It.IsAny<bool>())).ReturnsAsync(fake_media);

            var expected = _media_example;

            // Act
            var sut = new MediaController(media_repo_mock.Object, logger_mock.Object, media_adapter_mock.Object);
            var actual = await sut.GetMediaById(1) as OkObjectResult;
            var actual_result = actual.Value as Media;

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.AreEqual(expected.Type, actual_result.Type);
            Assert.AreEqual(expected.Title, actual_result.Title);
            Assert.AreEqual(expected.Year, actual_result.Year);
            Assert.AreEqual(expected.Runtime, actual_result.Runtime);
            Assert.AreEqual(expected.Director, actual_result.Director);
            Assert.AreEqual(expected.Actors, actual_result.Actors);
        }
    }
}