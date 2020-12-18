namespace TrendyMemes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TrendyMemes.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsUpvote { get; set; }
    }
}
