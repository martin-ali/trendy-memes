namespace TrendyMemes.Web.Areas.Posts.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TrendyMemes.Common.Attributes;

    public class PostCreateInputModel
    {
        [Required]
        [MinLengthWithCustomMessage(3)]
        [MaxLengthWithCustomMessage(20)]
        public string Title { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [MinLengthWithCustomMessage(2)]
        [MaxLengthWithCustomMessage(200)]
        public string Tags { get; set; }
    }
}
