namespace TrendyMemes.Data.Models
{
    using TrendyMemes.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        public bool IsUpvote { get; set; }
    }
}