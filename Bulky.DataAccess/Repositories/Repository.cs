using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories

{
    internal class Repository<T> : IRepository<T> where T : class
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

        public T? Get(Expression<Func<T, bool>> filter , string? includeProperty = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
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

        public IEnumerable<T> GetAll(string? includeProperty=null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (string include in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }
            return query.ToList();
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
