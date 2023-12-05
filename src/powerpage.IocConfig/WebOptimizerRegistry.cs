using Microsoft.Extensions.DependencyInjection;

namespace powerpage.IocConfig;

public static class WebOptimizerRegistry
{
    public static void AddWebOptimizerServices(this IServiceCollection services)
    {
        services.AddWebOptimizer(pipeline =>
                                 {
                                     // Creates a CSS and a JS bundle. Globbing patterns supported.
                                     pipeline.AddCssBundle("/css/site.min.css",
                                                           "wwwroot/lib/bootstrap/dist/css/bootstrap.min.css",
                                                           "wwwroot/lib/bootstrap4-rtl/bootstrap-rtl.css",
                                                           "wwwroot/lib/components-font-awesome/css/solid.min.css",
                                                           "wwwroot/lib/components-font-awesome/css/fontawesome.min.css",
                                                           "Content/custom.css"
                                                          )
                                             .UseContentRoot()
                                             .AdjustRelativePaths();
                                     pipeline.AddJavaScriptBundle("/js/site.min.js",
                                                                  "wwwroot/lib/jquery/dist/jquery.min.js",
                                                                  "wwwroot/lib/popperjs/dist/umd/popper.min.js",
                                                                  "wwwroot/lib/jquery-validation/dist/jquery.validate.min.js",
                                                                  "wwwroot/lib/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js",
                                                                  "wwwroot/lib/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js",
                                                                  "wwwroot/lib/bootstrap/dist/js/bootstrap.min.js",
                                                                  "scripts/jquery.bootstrap-modal-confirm.js",
                                                                  "scripts/jquery.bootstrap-modal-alert.js",
                                                                  "scripts/jquery.bootstrap-modal-ajax-form.js",
                                                                  "scripts/custom.js"
                                                                 )
                                             .UseContentRoot();

                                     // This will minify any JS and CSS file that isn't part of any bundle
                                     pipeline.MinifyCssFiles();
                                     pipeline.MinifyJsFiles();
                                 });
    }
}