namespace CSK.PersonalBlog.WebUI.Models
{
    public class ResponseModel<TEntity>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public TEntity Result { get; set; }
    }
}
