using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Api;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.GenreDtos;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Services.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_CA23.Services.Adapters_Tests
{
    [TestClass()]
    public class MediaAdapter_Tests
    {

        [TestMethod()]
        public void Bind_ShouldReturnMedia_WhenProvidedWithMediaRequest()
        {
            // Arrange
            var fake_MediaRequest = new MediaRequest()
            {
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

            var expected = new Media()
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

            // Act
            var sut = new MediaAdapter();
            Media actual = sut.Bind(fake_MediaRequest);

            // Assert
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Year, actual.Year);
            Assert.AreEqual(expected.Runtime, actual.Runtime);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Director, actual.Director);
            Assert.AreEqual(expected.Writer, actual.Writer);
            Assert.AreEqual(expected.Actors, actual.Actors);
            Assert.AreEqual(expected.Plot, actual.Plot);
            Assert.AreEqual(expected.Language, actual.Language);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Poster, actual.Poster);
            Assert.AreEqual(expected.imdbId, actual.imdbId);
        }


        [TestMethod()]
        public void Bind_ShouldReturnMediaDto_WhenProvidedWithMedia()
        {
            // Arrange
            var fake_Genres = new List<Genre>(){
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

            var fake_Media = new Media()
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
                imdbId = "tt0083658",
                imdbRating = 8.1,
                imdbVotes = 771_646,
                Genres= fake_Genres
            };

            var expected_Genres = new List<GenreDto>(){
                new GenreDto(fake_Genres[0]),
                new GenreDto(fake_Genres[1])
            };

            var expected = new MediaDto()
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
                imdbId = "tt0083658",
                imdbRating = 8.1,
                Genres = expected_Genres
            };

            // Act
            var sut = new MediaAdapter();
            MediaDto actual = sut.Bind(fake_Media);

            // Assert
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Year, actual.Year);
            Assert.AreEqual(expected.Runtime, actual.Runtime);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Director, actual.Director);
            Assert.AreEqual(expected.Writer, actual.Writer);
            Assert.AreEqual(expected.Actors, actual.Actors);
            Assert.AreEqual(expected.Plot, actual.Plot);
            Assert.AreEqual(expected.Language, actual.Language);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Poster, actual.Poster);
            Assert.AreEqual(expected.imdbId, actual.imdbId);
        }



        [TestMethod()]
        public void Bind_ShouldReturnMedia_WhenProvidedWithOmdbApiMediaAndGenreList()
        {
            // Arrange

            var fake_OmdbApiMedia = new OmdbApiMedia()
            {
                Title = "Blade Runner",
                Year = "1982",
                Released = "25 Jun 1982",
                Runtime = "117 min",
                Genre = "Action, Drama, Sci-Fi",
                Director = "Ridley Scott",
                Writer = "Hampton Fancher, David Webb Peoples, Philip K. Dick",
                Actors = "Harrison Ford, Rutger Hauer, Sean Young",
                Plot = "A blade runner must pursue and terminate four replicants who stole a ship in space and have returned to Earth to find their creator.",
                Language = "English, German, Cantonese, Japanese, Hungarian, Arabic, Korean",
                Country = "United States",
                Poster = "https://m.media-amazon.com/images/M/MV5BNzQzMzJhZTEtOWM4NS00MTdhLTg0YjgtMjM4MDRkZjUwZDBlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg",
                imdbRating = "8.1",
                imdbVotes = "771646",
                imdbID = "tt0083658",
                Type = "movie"
            };

            var fake_Genres = new List<Genre>(){
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


            var expected_Genres = new List<Genre>(){
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

            var expected = new Media()
            {
                Title = "Blade Runner",
                Year = "1982",
                Runtime = "117 min",
                Genres = expected_Genres,
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

            // Act
            var sut = new MediaAdapter();
            Media actual = sut.Bind(fake_OmdbApiMedia, fake_Genres);

            // Assert
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Year, actual.Year);
            Assert.AreEqual(expected.Runtime, actual.Runtime);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Director, actual.Director);
            Assert.AreEqual(expected.Writer, actual.Writer);
            Assert.AreEqual(expected.Actors, actual.Actors);
            Assert.AreEqual(expected.Plot, actual.Plot);
            Assert.AreEqual(expected.Language, actual.Language);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Poster, actual.Poster);
            Assert.AreEqual(expected.imdbId, actual.imdbId);
        }
    }

}