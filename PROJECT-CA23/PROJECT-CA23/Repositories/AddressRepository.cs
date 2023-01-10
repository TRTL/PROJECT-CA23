using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CA23Context _context;

        public AddressRepository(CA23Context context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            var address = await _context.Addresses.CountAsync();
            return address;
        }

        public int Create(Address address)  // NAUDOTI ASYNC AR NENAUDOTI ASYNC - STAI KUR KLAUSIMAS !
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return address.AddressId;
        }

        public async Task<bool> Exist(int id)
        {
            var address = await _context.Addresses.AnyAsync(a => a.AddressId == id);
            return address;
        }

        public IEnumerable<Address> Find(Expression<Func<Address, bool>> predicate)
        {
            return _context.Addresses.Where(predicate);
        }

        public async Task<Address> Get(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            return address;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            var addresses = await _context.Addresses.ToListAsync();
            return addresses;
        }

        public async Task Remove(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task<Address?> GetByUserId(int userId)
        {
            var a = _context.Addresses.Include(a => a.User);

            var address = await a.Where(a => a.UserId == userId)
                                 .FirstOrDefaultAsync();
            return address;
        }

    }
}
