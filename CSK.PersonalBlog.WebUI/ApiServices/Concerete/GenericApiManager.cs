using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.Business.StringInfos;
using CSK.PersonalBlog.Entities.Concrete;
using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Models.Accounts;
using CSK.PersonalBlog.WebUI.Tools.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ApiServices.Concerete
{
    public class GenericApiManager : IGenericApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericApiManager(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:51765/api/v1/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
        }

        public int GetActiveUserId()
        {
            var accountViewModel = _httpContextAccessor.HttpContext.Session.GetObject<AccountViewModel>("ActiveUser");
            return accountViewModel == null ? 0 : accountViewModel.Id;
        }

        public async Task<TEntity> GetAsync<TEntity>(string endPoint = null)
        {
            var httpResponseMessage = await _httpClient.GetAsync(endPoint);
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<TEntity>(responseContent);
            else
            {
                InsertLog(httpResponseMessage);
                return Activator.CreateInstance<TEntity>();
            }
        }

        public async Task<TEntity> PostAsync<TEntity>(string endPoint = null, object entity = null) where TEntity : class, new()
        {
            var jsonData = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, MimeType.JSON);
            var httpResponseMessage = await _httpClient.PostAsync(endPoint, stringContent);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
            {
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(responseContent);
            }
            else
            {
                InsertLog(httpResponseMessage);
                return null;
            }

        }

        public async Task<bool> PostAsync(string endPoint = null, object entity = null)
        {
            var jsonData = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, MimeType.JSON);
            var httpResponseMessage = await _httpClient.PostAsync(endPoint, stringContent);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
                return true;
            else
            {
                InsertLog(httpResponseMessage);
                return false;
            }
        }

        public async Task<bool> PutAsync(string endPoint = null, object entity = null)
        {
            var jsonData = JsonConvert.SerializeObject(entity);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, MimeType.JSON);

            var httpResponseMessage = await _httpClient.PutAsync(endPoint, stringContent);

            if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                return true;
            else
            {
                InsertLog(httpResponseMessage);
                return false;
            }
        }

        /// <summary>
        /// Address to goto which delete action
        /// </summary>
        /// <param name="url">Destination address</param>
        /// <returns>True or False</returns>
        public async Task<bool> DeleteAsync(string endPoint = null)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(endPoint);

            if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                return true;
            else
            {
                InsertLog(httpResponseMessage);
                return false;
            }
        }

        public async Task<string> GetFileAsync(string endPoint = null)
        {
            var httpResponseMessage = await _httpClient.GetAsync(endPoint);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var responseContentBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                string fileByte = Convert.ToBase64String(responseContentBytes);
                return fileByte;
            }
            else
            {
                InsertLog(httpResponseMessage);
                return string.Empty;
            }
        }

        public async Task<ByteArrayContent> ConverterByFileAsync(IFormFile formFile)
        {
            using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            var byteArrayContent = new ByteArrayContent(fileBytes);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);

            return byteArrayContent;
        }


        public void InsertLog(HttpResponseMessage httpResponseMessage)
        {
            //_loggerService.InsertAsync(new Log
            //{
            //    Path = $"{httpResponseMessage.RequestMessage.RequestUri}",
            //    ErrorMessage = $"Method: {httpResponseMessage.RequestMessage.Method.Method}, Reason: {httpResponseMessage.ReasonPhrase}",
            //    StackTrace = $"Transfer Encoding = {httpResponseMessage.Headers.TransferEncoding}, Server: {httpResponseMessage.Headers.Server}",
            //    AppUserId = GetActiveUserId()
            //});
        }
    }
}
