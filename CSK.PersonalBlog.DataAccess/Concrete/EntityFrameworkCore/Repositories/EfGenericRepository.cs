using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class, ITable, new()
    {
        private readonly PersonalBlogContext _context;
        public EfGenericRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expressions)
        {
            return await _context.Set<TEntity>().Where(expressions).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expressions, Expression<Func<TEntity, TKey>> keySelector)
        {
            return await _context.Set<TEntity>().Where(expressions).OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return await _context.Set<TEntity>().OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expressions)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expressions);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
