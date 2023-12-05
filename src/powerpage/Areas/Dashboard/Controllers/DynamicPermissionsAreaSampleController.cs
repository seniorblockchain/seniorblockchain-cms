using System.ComponentModel;
using powerpage.Services.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace powerpage.Areas.Dashboard.Controllers;


[Authorize(Policy = ConstantPolicies.DynamicPermission), Area(AreaConstants.DashboardArea),
 BreadCrumb(UseDefaultRouteUrl = true, Order = 0), DisplayName("کنترلر نمونه با سطح دسترسی پویا در یک ناحیه خاص")]
public class DynamicPermissionsAreaSampleController : Controller
{
    [DisplayName("ایندکس"), BreadCrumb(Order = 1)]
    public IActionResult Index()
    {
        return View();
    }
}