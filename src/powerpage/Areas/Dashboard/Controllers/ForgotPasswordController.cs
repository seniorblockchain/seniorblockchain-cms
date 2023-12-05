﻿using powerpage.Common.IdentityToolkit;
using powerpage.Entities.Identity;
using powerpage.Services.Contracts.Identity;
using powerpage.ViewModels.Identity;
using powerpage.ViewModels.Identity.Emails;
using powerpage.ViewModels.Identity.Settings;
using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace powerpage.Areas.Dashboard.Controllers;

[Area(AreaConstants.DashboardArea)]
[AllowAnonymous]
[BreadCrumb(Title = "بازیابی کلمه‌ی عبور", UseDefaultRouteUrl = true, Order = 0)]
public class ForgotPasswordController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
    private readonly IApplicationUserManager _userManager;

    public ForgotPasswordController(
        IApplicationUserManager userManager,
        IPasswordValidator<User> passwordValidator,
        IEmailSender emailSender,
        IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _passwordValidator = passwordValidator ?? throw new ArgumentNullException(nameof(passwordValidator));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
    }

    [BreadCrumb(Title = "تائید کلمه‌ی عبور فراموش شده", Order = 1)]
    public IActionResult ForgotPasswordConfirmation() => View();

    [BreadCrumb(Title = "ایندکس", Order = 1)]
    public IActionResult Index() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha]
    public async Task<IActionResult> Index(ForgotPasswordViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                return View("Error");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailSender.SendEmailAsync(
                                              model.Email,
                                              "بازیابی کلمه‌ی عبور",
                                              "~/Areas/Dashboard/Views/EmailTemplates/_PasswordReset.cshtml",
                                              new PasswordResetViewModel
                                              {
                                                  UserId = user.Id,
                                                  Token = code,
                                                  EmailSignature = _siteOptions.Value.Smtp.FromName,
                                                  MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString(),
                                              })
                ;

            return View("ForgotPasswordConfirmation");
        }

        return View(model);
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidatePassword(string password, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Json("ایمیل وارد شده معتبر نیست.");
        }

        var result = await _passwordValidator.ValidateAsync(
                                                            (UserManager<User>)_userManager,
                                                            user,
                                                            password);
        return Json(result.Succeeded ? "true" : result.DumpErrors(true));
    }

    [BreadCrumb(Title = "تغییر کلمه‌ی عبور", Order = 1)]
    public IActionResult ResetPassword(string code = null) => code == null ? View("Error") : View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        ModelState.AddModelError("", result.DumpErrors(true));

        return View();
    }

    [BreadCrumb(Title = "تائیدیه تغییر کلمه‌ی عبور", Order = 1)]
    public IActionResult ResetPasswordConfirmation() => View();
}