using Db.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Db;

public static class ServiceCollectionMixin
{
    public static IServiceCollection AddDbDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IDbContext, DbContext>();
        services.AddFluentMigratorCore().ConfigureRunner(rb =>
        {
            rb.AddSQLite();
            rb.WithGlobalConnectionString(DbContext.ConnectionString)
                .ScanIn(typeof(InitialCreate).Assembly).For.Migrations();
        });
        
        services.AddSingleton<ISourcesRepository, SourcesRepository>();
        
        return services;
    }
}