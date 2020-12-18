using Microsoft.AspNetCore.Mvc;

namespace TrendyMemes.Web.Controllers
{
    public class TagsController : BaseController
    {
        public IActionResult AllByTag(int id)
        {
            return this.View();
        }
    }
}
