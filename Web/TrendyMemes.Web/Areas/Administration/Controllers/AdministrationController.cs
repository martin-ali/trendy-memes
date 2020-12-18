﻿namespace TrendyMemes.Web.Areas.Administration.Controllers
{
    using TrendyMemes.Common;
    using TrendyMemes.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
