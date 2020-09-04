using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Tools.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Tools.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        public int Id { get; set; }
        public BlogImageType BlogImageType { get; set; } = BlogImageType.BlogHome;

        private readonly IGenericApiService _genericApiService;
        public ImageTagHelper(IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string blob = await _genericApiService.GetFileAsync($"blogs/GetImageByBlogId/{Id}");

            string html = string.Empty;

            switch (BlogImageType)
            {
                case BlogImageType.BlogHome:
                    html = $"<img class='card-img-top' src='data:image/jpeg;base64,{blob}'>";
                    break;
                case BlogImageType.BlogDetail:
                    html = $"<img class='img-fluid rounded' src='data:image/jpeg;base64,{blob}'>";
                    break;
            }
            output.Content.SetHtmlContent(html);
        }
    }
}
