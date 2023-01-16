using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_CA23_Tests.Database
{
    [TestClass()]
    public class Context_Tests
    {
        private CA23Context _context;

        [TestInitialize]
        public void OnInit()
        {
            var options = new DbContextOptionsBuilder<CA23Context>()
                                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                                .Options;

            _context = new CA23Context(options);
            _context.Add(new User()
            {
                UserId = 1,
                Username = "pertras3000",
                FirstName = "Petras",
                LastName = "Petraitis",
                Role = ERole.user,
                PasswordHash = new byte[8],
                PasswordSalt = new byte[8],
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
            _context.Add(new Media()
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
            });
            _context.Add(new Media()
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
            });
            _context.SaveChanges();
        }


        [TestMethod()]
        public void Context_Test1()
        {
            var userMedia1 = new UserMedia()
            {
                UserId = 1,
                MediaId = 1
            };
            var userMedia2 = new UserMedia()
            {
                UserId = 1,
                MediaId = 2
            };

            _context.Add(userMedia1);
            _context.Add(userMedia2);
            _context.SaveChanges();

            Assert.IsTrue(_context.UserMedias.Any(m => m.User.FirstName == "Petras"));
            Assert.AreEqual(_context.UserMedias.Where(m => m.UserId == 1).Count(), 2);
        }
    }
}
