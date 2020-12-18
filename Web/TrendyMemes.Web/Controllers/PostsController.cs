namespace TrendyMemes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        public IActionResult Details(int id)
        {
            return this.View();
        }
    }
}
