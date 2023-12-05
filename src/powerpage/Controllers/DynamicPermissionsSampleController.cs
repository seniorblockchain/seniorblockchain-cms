using System.ComponentModel;
using powerpage.Services.Identity;
using powerpage.ViewModels.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using powerpage.Framework;

namespace powerpage.Controllers;

[Authorize(Policy = ConstantPolicies.DynamicPermission), BreadCrumb(UseDefaultRouteUrl = true, Order = 0) ]

[LocalizedDisplayName("DynamicPermissionsSample")]

public class DynamicPermissionsSampleController : Controller
{
    [DisplayName("ایندکس"), BreadCrumb(Order = 1)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Index(RoleViewModel model)
    {
        return View(model);
    }

    [DisplayName("گزارش از لیست کتاب‌ها"), BreadCrumb(Order = 1)]
    public IActionResult Books()
    {
        return View("Index");
    }

    [DisplayName("گزارش از لیست مراجعان"), BreadCrumb(Order = 1)]
    public IActionResult Users()
    {
        return View("Index");
    }

    [DisplayName("گزارش از لیست امانات"), BreadCrumb(Order = 1)]
    public IActionResult BooksGiven()
    {
        return View("Index");
    }

    [DisplayName("گزارش از لیست مفقودی‌ها"), BreadCrumb(Order = 1)]
    public IActionResult BooksMissings()
    {
        return View("Index");
    }
}