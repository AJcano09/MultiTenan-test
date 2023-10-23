using Microsoft.AspNetCore.Http;
using MultiTenant.Domain.Entities;
using Multitenant.Infraestructure.Database.Organization;

namespace MultiTenant.Application.Repositories;
public class OrganizationRepository : GenericRepository<Organization>
{
    public OrganizationRepository(OrganizationsDbContext organizationConnection,IHttpContextAccessor contextAccessor) 
        : base(
            organizationConnection.GetConnection(string.Empty))
    { }
}