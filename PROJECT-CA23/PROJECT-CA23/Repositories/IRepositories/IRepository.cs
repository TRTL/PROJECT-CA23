using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, 
                                        ICollection<string>? includeTables = null, 
                                        Expression<Func<TEntity, dynamic>>? orderByColumn = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, ICollection<string> includeTables, bool tracked = true);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
