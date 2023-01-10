using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<int> Count();
        int Create(T entity);
        Task<bool> Exist(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Remove(T entity);
        Task Update(T entity);

    }
}
