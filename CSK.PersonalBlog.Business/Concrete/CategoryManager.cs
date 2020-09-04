using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(IGenericDal<Category> genericDal, ICategoryDal categoryDal) : base(genericDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<List<Category>> GetAllSortedByIdAsync()
        {
            return await _categoryDal.GetAllAsync(i => i.Id);
        }

        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            return await _categoryDal.GetAllWithCategoryBlogsAsync();
        }
    }
}
