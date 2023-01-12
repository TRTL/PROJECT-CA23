using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly CA23Context _db;

        public AddressRepository(CA23Context db) : base(db)
        {
            _db = db;
        }
    }
}
