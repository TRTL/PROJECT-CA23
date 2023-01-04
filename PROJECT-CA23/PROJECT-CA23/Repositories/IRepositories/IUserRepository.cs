using PROJECT_CA23.Models;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IUserRepository
    {
        bool Exist(string userName);
        int Register(User user);
        bool TryLogin(string userName, string password, out User? user);
    }
}
