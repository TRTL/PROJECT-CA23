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
    }
}
