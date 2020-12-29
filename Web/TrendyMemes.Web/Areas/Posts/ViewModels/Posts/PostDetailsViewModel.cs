namespace TrendyMemes.Web.Areas.Posts.ViewModels.Posts
{
    using System.Collections.Generic;

    using TrendyMemes.Data.Models;
    using TrendyMemes.Services.Mapping;
    using TrendyMemes.Web.Areas.Posts.ViewModels.Comments;

    public class PostDetailsViewModel : PostInListViewModel, IMapFrom<Post>
    {
        public PostDetailsViewModel()
            : base()
        {
            this.Comments = new List<CommentListViewModel>();
        }

        public IEnumerable<CommentListViewModel> Comments { get; set; }
    }
}
