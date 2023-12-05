using powerpage.DataLayer.Context;
using powerpage.DataLayer.InMemoryDatabase;
using powerpage.DataLayer.MSSQL;
using powerpage.DataLayer.SQLite;
using powerpage.Services.Contracts.Identity;
using powerpage.ViewModels.Identity.Settings;
using DNTCommon.Web.Core;
using Microsoft.Extensions.DependencyInjection;

namespace powerpage.IocConfig;

public static class DbContextOptionsExtensions
{
    public static IServiceCollection AddConfiguredDbContext(
        this IServiceCollection serviceCollection, SiteSettings siteSettings)
    {
        if (siteSettings == null)
        {
            throw new ArgumentNullException(nameof(siteSettings));
        }

        serviceCollection.AddInterceptors();

        switch (siteSettings.ActiveDatabase)
        {
            case ActiveDatabase.InMemoryDatabase:
                serviceCollection.AddConfiguredInMemoryDbContext(siteSettings);
                break;

            case ActiveDatabase.LocalDb:
            case ActiveDatabase.SqlServer:
                serviceCollection.AddConfiguredMsSqlDbContext(siteSettings);
                break;

            case ActiveDatabase.SQLite:
                serviceCollection.AddConfiguredSQLiteDbContext(siteSettings);
                break;

            default:
                throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file.");
        }

        return serviceCollection;
    }

    /// <summary>
    ///     Creates and seeds the database.
    /// </summary>
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        serviceProvider.RunScopedService<IIdentityDbInitializer>(identityDbInitialize =>
        {
            identityDbInitialize.Initialize();
            identityDbInitialize.SeedData();
        });
    }

    private static void AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<AuditableEntitiesInterceptor>();
    }
}