namespace TrendyMemes.Web.Areas.Posts.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Web.Areas.Posts.Services;
    using TrendyMemes.Web.Areas.Posts.Viewmodels;
    using TrendyMemes.Web.Areas.Posts.ViewModels;
    using TrendyMemes.Web.Controllers;

    [Area("Posts")]
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

        [HttpGet(nameof(Trendy))]
        public IActionResult Trendy()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInTrendyCategory);
            var viewModel = new PostsListViewModel
            {
                Posts = posts,
            };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet(nameof(Rising))]
        public IActionResult Rising()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInRisingCategory);
            var viewModel = new PostsListViewModel
            {
                Posts = posts,
            };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet(nameof(New))]
        public IActionResult New()
        {
            var posts = this.postsService.GetAll<PostInListViewModel>();
            var viewModel = new PostsListViewModel
            {
                Posts = posts,
            };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreatePostInputModel input)
        {
            return this.View(input);
        }
    }
}
