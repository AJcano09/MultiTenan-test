using Microsoft.AspNetCore.Http.Features;
using System.Data;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Database;
using Multitenant.Infraestructure.Database.Organization;
using Multitenant.Infraestructure.Database.Organization.Migrations;
using Multitenant.Infraestructure.Database.ProductByOrganization;

namespace Multitenant.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraEstructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ProductDbContext>();
        services.AddTransient<OrganizationsDbContext>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IDbConnection>( provider =>
        {
            var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var tenantName = httpContext?.Items["TenantName"]?.ToString();
            if (string.IsNullOrWhiteSpace(tenantName))
                throw new Exception("Tenant no identificado");
            var dbConnection = new ProductDbContext(new DatabaseConnectionFactory(configuration));
            return dbConnection.GetConnection(tenantName);
        });
        services.AddScoped<IDbConnection>( provider =>
        {
            var dbFactory = new OrganizationsDbContext(new DatabaseConnectionFactory(configuration));
            return dbFactory.GetConnection("organizations");
        });
        
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("UsersAndOrganizations"))
                .ScanIn(typeof(AddOrganizationTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole()).BuildServiceProvider();
        
        
        services.AddTransient<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        services.AddTransient<IDbContext, OrganizationsDbContext>();
        services.AddTransient<IDbContext, ProductDbContext>();
        return services;
    }
}