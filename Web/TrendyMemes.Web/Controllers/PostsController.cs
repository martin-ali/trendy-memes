namespace TrendyMemes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        public IActionResult Details(int id)
        {
            return this.View();
        }

        public IActionResult Trendy()
        {
            return this.View();
        }

        public IActionResult Rising()
        {
            return this.View();
        }

        public IActionResult New()
        {
            return this.View();
        }
    }
}
