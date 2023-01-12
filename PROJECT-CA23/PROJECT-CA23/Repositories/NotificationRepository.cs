using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;

namespace PROJECT_CA23.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly CA23Context _db;

        public NotificationRepository(CA23Context db) : base(db)
        {
            _db = db;
        }
    }
}
