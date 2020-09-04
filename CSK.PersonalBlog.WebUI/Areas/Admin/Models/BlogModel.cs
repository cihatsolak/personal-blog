using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CSK.PersonalBlog.WebUI.Areas.Admin.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }

        [Display(Name = "İçerik")]
        public string Description { get; set; }

        [Display(Name = "Görsel Seçiniz")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }
    }
}
