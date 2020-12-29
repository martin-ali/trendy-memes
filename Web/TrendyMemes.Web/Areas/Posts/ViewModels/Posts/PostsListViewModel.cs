namespace TrendyMemes.Web.Areas.Posts.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PostsListViewModel
    {
        public PostsListViewModel()
        {
            this.Posts = new List<PostInListViewModel>();
        }

        public IEnumerable<PostInListViewModel> Posts { get; set; }
    }
}
