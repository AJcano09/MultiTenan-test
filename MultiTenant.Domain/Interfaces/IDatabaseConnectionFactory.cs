using System.Data;

namespace MultiTenant.Domain.Interfaces;

public interface IDatabaseConnectionFactory
{
    IDbConnection CreateConnection(string dbProviders);
}