using System.Data;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Enums;

namespace Multitenant.Infraestructure.Database.ProductByOrganization;

public class ProductDbContext :IDbConnector
{
    private readonly IDatabaseConnectionFactory _connectionFactory;

    public ProductDbContext(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public IDbConnection GetConnection(string? tenantName)
        => _connectionFactory.CreateConnection(DbProvider.Products.ToString(),tenantName);

    
}