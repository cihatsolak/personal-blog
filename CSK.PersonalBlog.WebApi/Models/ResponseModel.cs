namespace CSK.PersonalBlog.WebApi.Models
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Result { get; set; }
    }
}
