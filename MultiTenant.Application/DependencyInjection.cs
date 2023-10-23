using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MultiTenant.Application.Repositories;
using MultiTenant.Application.Services;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;

namespace MultiTenant.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<Organization>();
        services.AddScoped<Product>();
        services.AddScoped<User>();
        services.AddScoped<UserRepository>();
        services.AddScoped<OrganizationRepository>();
        
        services.AddScoped<IGenericRepository<Organization>, OrganizationRepository>();
        services.AddScoped<IGenericRepository<Product>, ProductRepository>();
        services.AddScoped<IGenericRepository<User>, UserRepository>();
        services.AddTransient<IMigrationService, MigrationService>();
        return services;
    }
}