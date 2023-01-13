using PROJECT_CA23.Models;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IUserRepository
    {
        bool Exist(string userName);
        bool Exist(int id, out User? user);
        User Get(int id);
        IEnumerable<User> GetAll();
        int Register(User user);
        bool TryLogin(string userName, string password, out User? user);
        void Update(User user);
    }
}
