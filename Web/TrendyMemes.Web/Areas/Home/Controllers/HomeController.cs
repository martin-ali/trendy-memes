namespace TrendyMemes.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using TrendyMemes.Common;
    using TrendyMemes.Web.ViewModels;

    [Area(GlobalConstants.HomeArea)]
    public class HomeController : BaseController
    {
        [HttpGet("/")]
        [HttpGet("Home")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet(nameof(Privacy))]
        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet(nameof(Error))]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
