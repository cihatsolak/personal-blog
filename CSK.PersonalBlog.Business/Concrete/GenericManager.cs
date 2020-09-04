using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Concrete
{
    public class GenericManager<TEntity> : IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        private readonly IGenericDal<TEntity> _genericDal;
        public GenericManager(IGenericDal<TEntity> genericDal)
        {
            _genericDal = genericDal;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _genericDal.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _genericDal.GetAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _genericDal.InsertAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _genericDal.InsertAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _genericDal.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _genericDal.DeleteAsync(entity);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            await _genericDal.DeleteAsync(entities);
        }
    }
}
