using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Enums;

namespace MultiTenant.Application.Repositories;
public class OrganizationRepository : GenericRepository<Organization>
{
    public OrganizationRepository(IDatabaseConnectionFactory factory) 
        : base(factory.CreateConnection(DbProvider.UsersAndOrganizations.ToString()))
    { }
    
    
}