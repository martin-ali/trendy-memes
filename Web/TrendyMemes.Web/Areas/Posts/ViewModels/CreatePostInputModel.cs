namespace TrendyMemes.Web.Areas.Posts.Viewmodels
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePostInputModel
    {
        [Required]
        public string Title { get; set; }
    }
}
