using System;

namespace CSK.PersonalBlog.WebUI.Models.Blogs
{
    public class BlogViewModel : BaseViewModel<int>
    {
        public int AppuserId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
