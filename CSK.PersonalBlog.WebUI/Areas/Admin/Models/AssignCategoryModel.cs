namespace CSK.PersonalBlog.WebUI.Areas.Admin.Models
{
    public class AssignCategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Exists { get; set; }
    }
}
