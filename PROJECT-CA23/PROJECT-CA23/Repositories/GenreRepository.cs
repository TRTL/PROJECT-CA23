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

        public async Task<Genre?> FindByName(string genreName)
        {
            return await _db.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
        }
    }
}
