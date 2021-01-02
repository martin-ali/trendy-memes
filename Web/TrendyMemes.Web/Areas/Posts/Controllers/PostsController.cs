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
    using TrendyMemes.Web.Areas.Posts.ViewModels.Posts;
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
            var posts = this.postsService.GetTopPercent<PostInListViewModel>(GlobalConstants.PostsPercentageInNewCategory, 100);
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

        [HttpGet("ByUser")]
        public IActionResult ByUserId(string id)
        {
            var posts = this.postsService.GetByUserId<PostInListViewModel>(id);
            var viewModel = new PostsListViewModel { Posts = posts };

            return this.View(PostsListView, viewModel);
        }

        [HttpGet(nameof(Create))]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost(nameof(Create))]
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

        [HttpGet(nameof(Edit) + "/{id}")]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var post = this.postsService.GetById<PostEditInputModel>(id);

            return this.View(post);
        }

        [HttpPost(nameof(Edit) + "/{id}")]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, PostEditInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var tags = input.Tags.Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Distinct();
            await this.postsService.UpdateAsync(input, id, tags);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.postsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(HomeController.Index), ControllerHelpers.GetControllerName(nameof(HomeController)), new { area = GlobalConstants.HomeArea });
        }
    }
}
