﻿namespace TrendyMemes.Web.Areas.Tags.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TrendyMemes.Common;
    using TrendyMemes.Web.Controllers;

    [Area(GlobalConstants.PostsArea)]
    public class TagsController : BaseController
    {
        public IActionResult AllByTag(int id)
        {
            return this.View();
        }
    }
}