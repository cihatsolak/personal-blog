using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ApiServices.Interfaces
{
    public interface IGenericApiService
    {
        int GetActiveUserId();
        Task<TEntity> GetAsync<TEntity>(string endPoint = null);
        Task<string> GetFileAsync(string endPoint = null);
        Task<TEntity> PostAsync<TEntity>(string endPoint = null, object entity = null) where TEntity : class, new();
        Task<bool> PostAsync(string endPoint = null, object entity = null);
        Task<bool> PutAsync(string endPoint = null, object entity = null);
        Task<bool> DeleteAsync(string endPoint = null);
        Task<ByteArrayContent> ConverterByFileAsync(IFormFile formFile);
        void InsertLog(HttpResponseMessage httpResponseMessage);
    }
}
