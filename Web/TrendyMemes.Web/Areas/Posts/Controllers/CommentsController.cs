namespace TrendyMemes.Web.Areas.Posts.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Web.Areas.Posts.Services;
    using TrendyMemes.Web.Areas.Posts.ViewModels.Comments;
    using TrendyMemes.Web.Controllers;

    [Area(GlobalConstants.PostsArea)]
    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var author = await this.userManager.GetUserAsync(this.User);
                await this.commentsService.CreateAsync(input, author.Id);
            }

            return this.RedirectToAction(nameof(PostsController.Details), ControllerHelpers.GetControllerName(nameof(PostsController)), new
            {
                area = GlobalConstants.PostsArea,
                id = input.PostId,
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(CommentCreateInputModel input)
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
