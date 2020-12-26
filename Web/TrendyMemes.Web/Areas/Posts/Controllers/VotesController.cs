namespace TrendyMemes.Web.Areas.Posts.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Web.Areas.Posts.Services;

    [Area(GlobalConstants.PostsArea)]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVotesService votesService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [HttpGet(nameof(Upvote))]
        [Authorize]
        public async Task Upvote(int id)
        {
            await this.VoteOnPost(id, 1);
        }

        [HttpGet(nameof(Downvote))]
        [Authorize]
        public async Task Downvote(int id)
        {
            await this.VoteOnPost(id, -1);
        }

        private async Task VoteOnPost(int id, int value)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.votesService.VoteOnPostAsync(id, user.Id, value);
        }
    }
}
