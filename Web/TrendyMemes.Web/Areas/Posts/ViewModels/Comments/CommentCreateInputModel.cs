namespace TrendyMemes.Web.Areas.Comments.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
