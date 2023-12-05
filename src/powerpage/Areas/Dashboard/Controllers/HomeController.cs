using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace powerpage.Areas.Dashboard.Controllers;

[Area(AreaConstants.DashboardArea), Authorize, BreadCrumb(Title = "خانه", UseDefaultRouteUrl = true, Order = 0)]
public class HomeController : Controller
{
    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public IActionResult Index()
    {
        return View();
    }
}