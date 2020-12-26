namespace TrendyMemes.Web.Areas.Posts.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class PostCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [MinLength(2)]
        public string Tags { get; set; }
    }
}
