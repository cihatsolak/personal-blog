using CSK.PersonalBlog.WebUI.Models;

namespace CSK.PersonalBlog.WebUI.Models.Categories
{
    public class CategoryWithBlogsCountViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }
        public int BlogsCount { get; set; }
    }
}
