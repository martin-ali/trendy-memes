namespace TrendyMemes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class PostTag : BaseDeletableModel<int>
    {
        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
