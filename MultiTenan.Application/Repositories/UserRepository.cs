using Microsoft.AspNetCore.Http;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
namespace MultiTenant.Application.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(IDbConnector organizationConnection,IHttpContextAccessor contextAccessor) 
        : base(organizationConnection.GetConnection(contextAccessor.HttpContext.Items["TenantName"]?.ToString()))
    {
    }
}