using System.ComponentModel.DataAnnotations;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }
    }
}
