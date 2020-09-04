using CSK.PersonalBlog.WebUI.ApiServices.Interfaces;
using CSK.PersonalBlog.WebUI.Models;
using CSK.PersonalBlog.WebUI.Models.Categories;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.Tools.TagHelpers
{
    [HtmlTargetElement("blogfilter")]
    public class BlogFilterTagHelper : TagHelper
    {
        public int CategoryId { get; set; }
        public string SearchKeyword { get; set; }

        private readonly IGenericApiService _genericApiService;
        public BlogFilterTagHelper(IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder builder = new StringBuilder();

            if (CategoryId > 0)
            {
                ResponseModel<CategoryViewModel> responseModel;

                responseModel = await _genericApiService.GetAsync<ResponseModel<CategoryViewModel>>($"categories/{CategoryId}");

                builder.Append("<div class='border border-dark p-3 mb-2'>");
                builder.Append("Şuanda <strong>");
                builder.Append(responseModel.Result.Name);
                builder.Append("</strong> kategorisiyle ilgileniyorsunuz.");
                builder.Append("<a href='/Blog/List' class='float-right'>Filtreyi Kaldır</a></div>");
            }
            else if (!string.IsNullOrWhiteSpace(SearchKeyword))
            {
                builder.Append("<div class='border border-dark p-3 mb-2'>");
                builder.Append("Şuanda <strong>");
                builder.Append($"{SearchKeyword}</strong> için gönderiler listeleniyor.");
                builder.Append("<a href='/Blog/List' class='float-right'>Filtreyi Kaldır</a></div>");
            }

            output.Content.SetHtmlContent(builder.ToString());
        }
    }
}
