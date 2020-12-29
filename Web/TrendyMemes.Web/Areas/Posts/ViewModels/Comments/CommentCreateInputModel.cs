namespace TrendyMemes.Web.Areas.Posts.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Common.Attributes;

    public class CommentCreateInputModel
    {
        [Required]
        [MinLengthWithCustomMessage(3)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
