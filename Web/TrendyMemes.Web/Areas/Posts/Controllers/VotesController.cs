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
        public async Task<int> Upvote(int id)
        {
            var rating = await this.VoteOnPost(id, 1);

            return rating;
        }

        [HttpGet(nameof(Downvote))]
        [Authorize]
        public async Task<int> Downvote(int id)
        {
            var rating = await this.VoteOnPost(id, -1);

            return rating;
        }

        [NonAction]
        private async Task<int> VoteOnPost(int id, int value)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var rating = await this.votesService.VoteOnPostAsync(id, user.Id, value);

            return rating;
        }
    }
}
