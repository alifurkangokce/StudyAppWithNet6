using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Study.Core.Repositories;

namespace Study.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
            
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
           await _dbset.AddRangeAsync(entities);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
          return await _dbset.AnyAsync(expression);
        }
        
        public IQueryable<T> GetAll()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
                 
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
           _dbset.RemoveRange(entity);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }
    }
}
