namespace TrendyMemes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TagsController : BaseController
    {
        public IActionResult AllByTag(int id)
        {
            return this.View();
        }
    }
}
