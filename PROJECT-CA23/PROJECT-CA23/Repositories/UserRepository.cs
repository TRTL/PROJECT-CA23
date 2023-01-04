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

        public virtual bool TryLogin(string userName, string password, out User? user)
        {
            user = _context.Users.FirstOrDefault(x => x.UserName == userName);
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

        public bool Exist(string userName)
        {
            return _context.Users.Any(x => x.UserName == userName);
        }
    }
}
