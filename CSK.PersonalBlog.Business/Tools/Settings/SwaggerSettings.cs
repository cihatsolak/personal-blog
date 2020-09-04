namespace CSK.PersonalBlog.Business.Tools.Settings
{
    public class SwaggerSettings
    {
        public SwaggerDoc SwaggerDoc { get; set; }
        public SecurityDefinition SecurityDefinition { get; set; }
        public SecurityScheme SecurityScheme { get; set; }
    }
    public class SwaggerDoc
    {
        public string DocName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Url { get; set; }
    }

    public class SecurityDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BearerFormat { get; set; }
        public string Scheme { get; set; }
    }
    public class SecurityScheme
    {
        public string Id { get; set; }
    }
}
