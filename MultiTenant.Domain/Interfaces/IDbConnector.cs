using System.Data;

namespace MultiTenant.Domain.Interfaces;

public interface IDbConnector
{
    IDbConnection GetConnection(string? dbName);
}