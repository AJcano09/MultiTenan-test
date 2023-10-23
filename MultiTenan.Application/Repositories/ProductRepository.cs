using Microsoft.AspNetCore.Http;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Database.ProductByOrganization;

namespace MultiTenant.Application.Repositories;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(ProductDbContext context,IHttpContextAccessor contextAccessor)
        : base(context.GetConnection(contextAccessor.HttpContext.Items["TenantName"]?.ToString()))
    {
    }
}