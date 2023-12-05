using powerpage.Common.PersianToolkit;
using powerpage.DataLayer.Context;
using powerpage.ViewModels.Identity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace powerpage.DataLayer.InMemoryDatabase;

public static class InMemoryDatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredInMemoryDbContext(this IServiceCollection services,
        SiteSettings siteSettings)
    {
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        services.AddDbContextPool<ApplicationDbContext, InMemoryDatabaseContext>(
            (serviceProvider, optionsBuilder) =>
                optionsBuilder.UseConfiguredInMemoryDatabase(siteSettings, serviceProvider));
        return services;
    }

    public static void UseConfiguredInMemoryDatabase(
        this DbContextOptionsBuilder optionsBuilder, SiteSettings siteSettings, IServiceProvider serviceProvider)
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        optionsBuilder.UseInMemoryDatabase(siteSettings.ConnectionStrings.LocalDb.InitialCatalog);
        optionsBuilder.AddInterceptors(new PersianYeKeCommandInterceptor(),
            serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>());
        optionsBuilder.ConfigureWarnings(warnings =>
        {
            // ...
        });
    }
}