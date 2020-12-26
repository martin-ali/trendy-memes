namespace TrendyMemes.Web.Areas.Posts.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Web.Areas.Posts.Services;
    using TrendyMemes.Web.Areas.Posts.ViewModels;
    using TrendyMemes.Web.Controllers;

    [Area(GlobalConstants.PostsArea)]
    public class PostsController : BaseController
    {
        private const string PostsListView = "List";

        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(IPostsService postsService, UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.userManager = userManager;
        }

        [HttpGet(nameof(Details))]
        public IActionResult Details(int id)
        {
            var post = this.postsService.GetById<PostDetailsViewModel>(id);

            return this.View(post);
        }

        [HttpGet(nameof(Trendy))]
        public IActionResult Trendy()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(0, GlobalConstants.TopPostsPercentageInTrendyCategory);
            var viewModel = new PostsListViewModel { Posts = posts };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet(nameof(Rising))]
        public IActionResult Rising()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInTrendyCategory, GlobalConstants.TopPostsPercentageInRisingCategory);
            var viewModel = new PostsListViewModel { Posts = posts };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet(nameof(New))]
        public IActionResult New()
        {
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.TopPostsPercentageInRisingCategory, GlobalConstants.PostsPercentageInNewCategory);
            var viewModel = new PostsListViewModel { Posts = posts };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet("ByTag")]
        public IActionResult ByTagId(int id)
        {
            var posts = this.postsService.GetByTagId<PostInListViewModel>(id);
            var viewModel = new PostsListViewModel { Posts = posts };

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
        public async Task<IActionResult> Create(PostCreateInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var author = await this.userManager.GetUserAsync(this.User);
            var tags = input.Tags.Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Distinct();
            var newPostId = await this.postsService.CreateAsync(input, author.Id, tags);

            return this.RedirectToAction(nameof(this.Details), new { id = newPostId });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit()
        {
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete()
        {
            return this.View();
        }
    }
}
