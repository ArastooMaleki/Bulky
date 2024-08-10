using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Bulky.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext) 
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter , string? includeProperty = null , bool tracked = true)
        {
            IQueryable<T> query;
            if (tracked == true)
            {
                query = _dbSet.AsQueryable();
            }
            else
            {
                query = _dbSet.AsQueryable().AsNoTracking();
            }
            
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (string include in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll( string? includeProperty=null, Expression<Func<T, bool>>? filter = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (string include in includeProperty.Split(new char[] { ',' }, StringSplitOptions.TrimEntries))
                {
                    query = query.Include(include);
                }
            }
            if (filter == null){
                return query;
            }
            else
            {
                return query.Where(filter);
            }
            
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
