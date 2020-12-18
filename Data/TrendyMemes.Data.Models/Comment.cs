namespace TrendyMemes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [MinLength(3)]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
