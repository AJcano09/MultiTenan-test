using Dapper;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure;
using Multitenant.Infraestructure.Database.Organization;
using Multitenant.Infraestructure.Database.Organization.Migrations;
using Multitenant.Infraestructure.Database.ProductByOrganization;
using Multitenant.Infraestructure.Database.ProductByOrganization.Migrations;
using Npgsql;

namespace MultiTenant.Application.Services;

public class MigrationService : IMigrationService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _contextAccessor;

    public MigrationService(IConfiguration configuration,IHttpContextAccessor contextAccessor)
    {
        _configuration = configuration;
        _contextAccessor = contextAccessor;
    }
    public void RunMigrationForOrganization(string? dataBaseName)
    {
        var connectionString =  _configuration.GetConnectionString("UsersAndOrganizations");
        EnsureDatabaseExists(connectionString);
        var serviceProvider = CreateServicesForOrganization(connectionString);
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>(); 
        runner.MigrateUp();
    }
    
    public void RunMigrationForProducts()
    {
        var dbNameFromTenant = _contextAccessor.HttpContext.Items[Constants.SLUG_TENANT].ToString() ;
        var connectionString = _configuration.GetConnectionString("Products"); 
        var tenantConnectionString = string.Format(connectionString, dbNameFromTenant );
        EnsureDatabaseExists(connectionString);
        var serviceProvider = CreateServicesForProduct(tenantConnectionString);
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
    private static void EnsureDatabaseExists(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var tempDatabaseName = builder.Database;
       
        builder.Database = "postgres";

        using var connection = new NpgsqlConnection(builder.ConnectionString);
        connection.Open();
        var exists = connection.ExecuteScalar<bool>($"SELECT EXISTS (SELECT FROM pg_database WHERE datname = '{tempDatabaseName}');");
        if (!exists)
        {
            connection.Execute($"CREATE DATABASE {tempDatabaseName};");
        }
    }

    private static IServiceProvider CreateServicesForProduct(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(AddProductTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
    }
    private static IServiceProvider CreateServicesForOrganization(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(AddOrganizationTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
    }

  
}