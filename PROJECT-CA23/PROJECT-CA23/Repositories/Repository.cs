using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;
using PROJECT_CA23.Database;
using PROJECT_CA23.Repositories.IRepositories;
using System.Linq.Expressions;

namespace PROJECT_CA23.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly CA23Context _db;
        private DbSet<TEntity> _dbSet;

        public Repository(CA23Context db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter == null) throw new NotImplementedException();
            return await query.AnyAsync(filter);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, 
                                                     ICollection<string>? includeTables = null,
                                                     Expression<Func<TEntity, dynamic>>? orderByColumn = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null) query = query.Where(filter);

            if (includeTables != null)
            foreach (var tableName in includeTables)
            {
                query = query.Include(tableName);
            }

            if (orderByColumn != null)  query = query.OrderBy(orderByColumn);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!tracked) query = query.AsNoTracking();
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, ICollection<string> includeTables, bool tracked = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!tracked) query = query.AsNoTracking();
            query = query.Where(filter);

            foreach (var tableName in includeTables)
            {
                query = query.Include(tableName);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
