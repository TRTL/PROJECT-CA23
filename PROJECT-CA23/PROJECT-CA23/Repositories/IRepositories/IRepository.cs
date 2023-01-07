using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        int Create(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> Count();
        Task<bool> Exist(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    }
}
