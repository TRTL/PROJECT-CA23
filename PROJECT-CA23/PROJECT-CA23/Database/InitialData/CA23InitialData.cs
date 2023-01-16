using Microsoft.AspNetCore.Http.HttpResults;
using PROJECT_CA23.Controllers;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services;
using PROJECT_CA23.Services.IServices;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace PROJECT_CA23.Database.InitialData
{
    public class CA23InitialData
    {
        public static byte[] CreateInitialPassword(out byte[] passwordSalt)
        {
            byte[] passwordHash = new byte[8];
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("admin"));
            }
            return passwordHash;
        }

        public static readonly User[] userInitialDataSeed = new User[]
        {
            new User()
            {                
                UserId = 1,
                Username = "admin",
                FirstName = "Jonas",
                LastName = "Jonaitis",
                Role = ERole.admin,
                PasswordHash = CreateInitialPassword(out byte[] passwordSalt),
                PasswordSalt = passwordSalt,
                Created = DateTime.Now,
                Updated = DateTime.Now
            }
        };

        public static readonly Address[] addressInitialDataSeed = new Address[]
        {
            new Address()
            {
                AddressId = 1,
                UserId = 1,
                Country = "Country X1",
                City = "City X1",
                AddressText = "Address X1",
                PostCode = "PostCode X1"
            }
        };

        public static readonly Genre[] genreInitialDataSeed = new Genre[]
        {
            new Genre()
            {
                GenreId = 1,
                Name = "Action"
            },
            new Genre()
            {
                GenreId = 2,
                Name = "Drama"
            },
            new Genre()
            {
                GenreId = 3,
                Name = "Sci-Fi"
            },
            new Genre()
            {
                GenreId = 4,
                Name = "Crime"
            },
            new Genre()
            {
                GenreId = 5,
                Name = "Thriller"
            }
        };

        public static readonly Media[] mediaInitialDataSeed = new Media[]
        {
            new Media()
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
                imdbVotes = 771_646
            },
            new Media()
            {
                MediaId = 2,
                Type = "series",
                Title = "Breaking Bad",
                Year = "2008–2013",
                Runtime = "49 min",
                Director = null,
                Writer = "Vince Gilligan",
                Actors = "Bryan Cranston, Aaron Paul, Anna Gunn",
                Plot = "A chemistry teacher diagnosed with inoperable lung cancer turns to manufacturing and selling methamphetamine with a former student in order to secure his family's future.",
                Language = "English",
                Country = "United States",
                Poster = "https://m.media-amazon.com/images/M/MV5BYTU3NWI5OGMtZmZhNy00MjVmLTk1YzAtZjA3ZDA3NzcyNDUxXkEyXkFqcGdeQXVyODY5Njk4Njc@._V1_SX300.jpg",
                imdbId = "tt0903747",
                imdbRating = 9.5,
                imdbVotes = 1_880_303
            }
        };

        public static readonly object[] mediaGenreInitialDataSeed = new object[]
        {
            new {  MediasMediaId = 1, GenresGenreId = 1 },
            new {  MediasMediaId = 1, GenresGenreId = 2 },
            new {  MediasMediaId = 1, GenresGenreId = 3 },
            new {  MediasMediaId = 2, GenresGenreId = 4 },
            new {  MediasMediaId = 2, GenresGenreId = 2 },
            new {  MediasMediaId = 2, GenresGenreId = 5 }
        };
    }    
}
