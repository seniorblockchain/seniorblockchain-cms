using powerpage.IocConfig;
using powerpage.Services.Identity.Logger;
using powerpage.ViewModels.Identity.Settings;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging(builder.Logging, builder.Environment, builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);
var webApp = builder.Build();
ConfigureMiddlewares(webApp, webApp.Environment);
ConfigureEndpoints(webApp);
ConfigureDatabase(webApp);
webApp.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
{
    services.Configure<SiteSettings>(options => configuration.Bind(options));
    services.Configure<ContentSecurityPolicyConfig>(options =>
                                                        configuration.GetSection("ContentSecurityPolicyConfig")
                                                                     .Bind(options));

    // Adds all of the ASP.NET Core Identity related services and configurations at once.
    services.AddCustomIdentityServices(configuration);

    services.AddMvc(options => options.UseYeKeModelBinder());

    services.AddDNTCommonWeb();
    services.AddDNTCaptcha(options =>
                           {
                               options.UseCookieStorageProvider()
                                      .AbsoluteExpiration(7)
                                      .ShowExceptionsInResponse(env.IsDevelopment())
                                      .ShowThousandsSeparators(false)
                                      .WithEncryptionKey("This is my secure key!");
                           });
    services.AddCloudscribePagination();
    services.AddWebOptimizerServices();

    services.AddControllersWithViews(options => { options.Filters.Add(typeof(ApplyCorrectYeKeFilterAttribute)); });
    services.AddRazorPages();


    var supportedCultures = new[]
             {
                 new CultureInfo("ar-OM"),
                 new CultureInfo("en-US"),
                 new CultureInfo("fa-IR"),
                 new CultureInfo("tr-TR"),
             };
    var cultures = supportedCultures;

    services.Configure<RequestLocalizationOptions>(options =>
    {
        var englishCulture = Array.Find(cultures, x => string.Equals(x.Name, "en-US", StringComparison.Ordinal));
        options.DefaultRequestCulture = new RequestCulture(englishCulture?.Name ?? "ar-OM");
        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;
    });



}

void ConfigureLogging(ILoggingBuilder logging, IHostEnvironment env, IConfiguration configuration)
{
    logging.ClearProviders();

    if (env.IsDevelopment())
    {
        logging.AddDebug();
        logging.AddConsole();
    }

    logging.AddConfiguration(configuration.GetSection("Logging"));
    logging.AddDbLogger(); // You can change its Log Level using the `appsettings.json` file -> Logging -> LogLevel -> Default
}

void ConfigureMiddlewares(IApplicationBuilder app, IHostEnvironment env)
{
    if (!env.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseWebOptimizer();

    app.UseHttpsRedirection();
    app.UseExceptionHandler("/error/index/500");
    app.UseStatusCodePagesWithReExecute("/error/index/{0}");

    app.UseContentSecurityPolicy();

    app.UseStaticFiles();

    app.UseRouting();
    app.UseRateLimiter();

    app.UseAuthentication();
    app.UseAuthorization();
}

void ConfigureEndpoints(IApplicationBuilder app)
{
    app.UseEndpoints(endpoints =>
                     {
                         endpoints.MapControllers();

                         endpoints.MapControllerRoute(
                                                      "areaRoute",
                                                      "{area:exists}/{controller=Account}/{action=Index}/{id?}");

                         endpoints.MapControllerRoute(
                                                      "default",
                                                      "{controller=Home}/{action=Index}/{id?}");

                         endpoints.MapRazorPages();
                     });
}

void ConfigureDatabase(IApplicationBuilder app)
{
    app.ApplicationServices.InitializeDb();
}