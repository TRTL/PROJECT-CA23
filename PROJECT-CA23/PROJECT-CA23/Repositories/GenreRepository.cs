using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;

namespace PROJECT_CA23.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly CA23Context _db;

        public GenreRepository(CA23Context db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Genre> GetAllMediaOfSpecificGenre(int genreId)
        {
            //var query = from genre in _db.Genres
            //            join media in _db.Medias on genre.
            //            where genre.Medias.Where(m => m.Genres.Select(g => g.GenreId == genreId))
            //            select genre;








            var genreWithAllItsMedias = _db.Genres.Where(g => g.GenreId == genreId).Include("Medias");
            return genreWithAllItsMedias;
        }
    }
}
