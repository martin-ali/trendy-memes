namespace TrendyMemes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Services.Data;
    using TrendyMemes.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private const string PostsListView = "All";

        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var post = this.postsService.GetById<PostDetailsViewModel>(id);

            return this.View(post);
        }

        [HttpGet]
        public IActionResult Trendy()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInTrendyCategory);

            return this.View(PostsListView, posts);
        }

        [HttpGet]
        public IActionResult Rising()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInRisingCategory);

            return this.View(PostsListView, posts);
        }

        [HttpGet]
        public IActionResult New()
        {
            var posts = this.postsService.GetAll<PostInListViewModel>();

            return this.View(PostsListView, posts);
        }
    }
}
