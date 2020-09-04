using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ApiServices.Concerete
{
    public class BlogApiManager : GenericApiManager, IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:51765/api/v1/blogs/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
        }

        public async Task<bool> InsertAsync(BlogModel blogModel)
        {
            blogModel.AppUserId = GetActiveUserId();

            var formData = new MultipartFormDataContent();

            var imageByteArray = await ConverterByFileAsync(blogModel.ImageFile);

            formData.Add(imageByteArray, nameof(blogModel.ImageFile), blogModel.ImageFile.FileName);
            formData.Add(new StringContent(blogModel.AppUserId.ToString()), nameof(blogModel.AppUserId));
            formData.Add(new StringContent(blogModel.ShortDescription), nameof(blogModel.ShortDescription));
            formData.Add(new StringContent(blogModel.Description), nameof(blogModel.Description));
            formData.Add(new StringContent(blogModel.Title), nameof(blogModel.Title));

            var httpResponseMessage = await _httpClient.PostAsync("Create", formData);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
                return true;

            return false;
        }

        public async Task<bool> UpdateAsync(BlogModel blogModel)
        {
            blogModel.AppUserId = GetActiveUserId();

            var formData = new MultipartFormDataContent();

            if (blogModel.ImageFile != null)
            {
                var imageByteArray = await ConverterByFileAsync(blogModel.ImageFile);
                formData.Add(imageByteArray, nameof(blogModel.ImageFile), blogModel.ImageFile.FileName);
            }

            formData.Add(new StringContent(nameof(blogModel.Id)), nameof(blogModel.Id));
            formData.Add(new StringContent(blogModel.AppUserId.ToString()), nameof(blogModel.AppUserId));
            formData.Add(new StringContent(blogModel.ShortDescription), nameof(blogModel.ShortDescription));
            formData.Add(new StringContent(blogModel.Description), nameof(blogModel.Description));
            formData.Add(new StringContent(blogModel.Title), nameof(blogModel.Title));

            var httpResponseMessage = await _httpClient.PutAsync($"Update/{blogModel.Id}", formData);

            if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                return true;

            return false;
        }
    }
}
