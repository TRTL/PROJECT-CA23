using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;

namespace PROJECT_CA23.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly CA23Context _db;

        public ReviewRepository(CA23Context db) : base(db)
        {
            _db = db;
        }
    }
}
