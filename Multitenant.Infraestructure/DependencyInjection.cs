
using System.Data;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Database;
using Multitenant.Infraestructure.Database.Organization.Migrations;

namespace Multitenant.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraEstructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IDbConnection>( provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var factory =new DatabaseConnectionFactory(configuration,httpContextAccessor);
            return factory.CreateConnection("Products");
        });
        services.AddScoped<IDbConnection>( provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var dbFactory =new DatabaseConnectionFactory(configuration,httpContextAccessor);
            return dbFactory.CreateConnection("organizations");
        });
        
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("UsersAndOrganizations"))
                .ScanIn(typeof(AddOrganizationTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole()).BuildServiceProvider();
        
        
        services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        return services;
    }
}