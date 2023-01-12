using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;

namespace PROJECT_CA23.Repositories
{
    public class UserMediaRepository : Repository<UserMedia>, IUserMediaRepository
    {
        private readonly CA23Context _db;

        public UserMediaRepository(CA23Context db) : base(db)
        {
            _db = db;
        }
    }
}
