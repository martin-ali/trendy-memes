namespace TrendyMemes.Web.Areas.Identity.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Web.Areas.Identity.Services;
    using TrendyMemes.Web.Controllers;

    [Area(GlobalConstants.IdentityArea)]
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Details(string id)
        {
            var user = this.usersService.GetProfileById(id);

            return this.View(user);
        }
    }
}
