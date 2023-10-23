using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Application.Repositories;
using MultiTenant.Application.Services;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Database.Organization;
using Multitenant.Infraestructure.Database.ProductByOrganization;

namespace MultiTenant.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<Organization>();
        services.AddTransient<Product>();
        services.AddTransient<User>();
        services.AddTransient<UserRepository>();
        services.AddTransient<OrganizationRepository>();
        
        services.AddTransient<IGenericRepository<Organization>, OrganizationRepository>();
        services.AddTransient<IGenericRepository<Product>, ProductRepository>();
        services.AddTransient<IGenericRepository<User>, UserRepository>();
        services.AddTransient<IDbContext, ProductDbContext>();
        services.AddTransient<IDbContext, OrganizationsDbContext>();
        services.AddTransient<IMigrationService, MigrationService>();
        return services;
    }
}