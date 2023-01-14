using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly CA23Context _db;

        public GenreRepository(CA23Context db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Genre>?> AddNewAndGetExistingGenresOfThisMedia(string genreString)
        {
            if (genreString.IsNullOrEmpty()) return null;

            var genreArr = genreString.Split(", ");

            List<Genre> listOfGenres = new List<Genre>();

            foreach (var genre in genreArr)
            {
                var foundGenre = await _db.Genres.FirstOrDefaultAsync(g => g.Name == genre);
                if (foundGenre != null)
                {
                    listOfGenres.Add(foundGenre);
                }
                else
                {
                    var newGenre = new Genre()
                    {
                        Name = genre
                    };
                    _db.Genres.Add(newGenre);
                    await _db.SaveChangesAsync();
                    listOfGenres.Add(newGenre);
                }
            }
            return listOfGenres;
        }


    }
}
