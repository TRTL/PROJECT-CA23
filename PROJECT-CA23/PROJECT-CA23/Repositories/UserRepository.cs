using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.IServices;

namespace PROJECT_CA23.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CA23Context _context;
        private readonly IUserService _userService;

        public UserRepository(CA23Context context,
                              IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public virtual bool TryLogin(string username, string password, out User? user)
        {
            user = _context.Users.FirstOrDefault(x => x.Username == username);
            if (user == null) return false;
            if (!_userService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return false;
            return true;
        }

        public int Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public bool Exist(string username)
        {
            return _context.Users.Any(x => x.Username == username);
        }

        public bool Exist(int id, out User? user)
        {
            user = null;
            if (!_context.Users.Any(x => x.UserId == id)) return false;
            user = _context.Users.FirstOrDefault(x => x.UserId == id);
            return true;
        }

        public User Get(int id)
        { 
            return _context.Users.Include("Address").First(c => c.UserId == id); 
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }


    }
}
