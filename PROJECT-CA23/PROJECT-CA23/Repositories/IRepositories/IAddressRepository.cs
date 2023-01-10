using PROJECT_CA23.Models;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address?> GetByUserId(int userId);
    }
}
