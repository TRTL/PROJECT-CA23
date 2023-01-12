using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Task<int> Count();
        //int Create(T entity);
        //Task<bool> Exist(int id);
        //IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        //Task<T> Get(int id);
        //Task<IEnumerable<T>> GetAll();
        //Task Remove(T entity);
        //Task Update(T entity);
        Task CreateAsync(TEntity entity);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter, ICollection<string> includeTables);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, ICollection<string> includeTables, bool tracked = true);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
