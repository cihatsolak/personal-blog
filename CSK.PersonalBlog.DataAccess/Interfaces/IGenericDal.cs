using CSK.PersonalBlog.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.DataAccess.Interfaces
{
    public interface IGenericDal<TEntity> where TEntity : class, ITable, new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expressions);
        Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expressions, Expression<Func<TEntity, TKey>> keySelector);        
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expressions);
        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
