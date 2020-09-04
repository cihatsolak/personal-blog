using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ApiServices.Interfaces
{
    public interface IBlogApiService : IGenericApiService
    {
        Task<bool> InsertAsync(BlogModel blogModel);
        Task<bool> UpdateAsync(BlogModel blogModel);
    }
}
