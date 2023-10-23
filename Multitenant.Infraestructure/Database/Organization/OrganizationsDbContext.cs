using System.Data;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Enums;

namespace Multitenant.Infraestructure.Database.Organization;

public class OrganizationsDbContext : IDbContext
{
    private readonly IDatabaseConnectionFactory _connectionFactory;
    public OrganizationsDbContext(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IDbConnection GetConnection(string? tenantName)
        => _connectionFactory.CreateConnection(DbProvider.UsersAndOrganizations.ToString(),tenantName);
}