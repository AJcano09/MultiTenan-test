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
        services.AddTransient<Organization>();
        services.AddTransient<Product>();
        services.AddTransient<User>();
        services.AddTransient<UserRepository>();
        services.AddTransient<OrganizationRepository>();
        
        services.AddTransient<IGenericRepository<Organization>, OrganizationRepository>();
        services.AddTransient<IGenericRepository<Product>, ProductRepository>();
        services.AddTransient<IGenericRepository<User>, UserRepository>();
        services.AddTransient<IMigrationService, MigrationService>();
        return services;
    }
}