namespace TrendyMemes.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Data.Models;
    using TrendyMemes.Web.Controllers;

    // [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(GlobalConstants.AdministrationArea)]
    public class AdministrationController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> BecomeAdmin()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

            return this.RedirectToAction(nameof(HomeController), ControllerHelpers.GetControllerName(nameof(HomeController)));
        }
    }
}
