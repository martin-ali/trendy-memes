namespace TrendyMemes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Services.Data;
    using TrendyMemes.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;

        private const string PostsListView = "All";

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public IActionResult Details(int id)
        {
            var post = this.postsService.GetById<PostDetailsViewModel>(id);

            return this.View(post);
        }

        public IActionResult Trendy()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInTrendyCategory);

            return this.View(PostsListView, posts);
        }

        public IActionResult Rising()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInRisingCategory);

            return this.View(PostsListView, posts);
        }

        public IActionResult New()
        {
            var posts = this.postsService.GetAll<PostInListViewModel>();

            return this.View(PostsListView, posts);
        }
    }
}
