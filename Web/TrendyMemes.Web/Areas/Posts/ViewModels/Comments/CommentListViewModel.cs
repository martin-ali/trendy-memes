namespace TrendyMemes.Web.Areas.Posts.ViewModels.Comments
{
    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;

    public class CommentListViewModel : IMapFrom<Comment>
    {
        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string Content { get; set; }
    }
}
