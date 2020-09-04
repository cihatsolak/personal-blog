using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.WebUI.ViewComponents
{
    public class Search : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() => View());
        }
    }
}
