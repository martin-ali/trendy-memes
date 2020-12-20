namespace TrendyMemes.Web.Areas.Tags.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Web.Controllers;

    public class TagsController : BaseController
    {
        public IActionResult AllByTag(int id)
        {
            return this.View();
        }
    }
}
