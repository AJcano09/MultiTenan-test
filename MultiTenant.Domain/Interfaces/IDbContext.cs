using System.Data;

namespace MultiTenant.Domain.Interfaces;

public interface IDbContext
{
    IDbConnection GetConnection(string? dbName);
}