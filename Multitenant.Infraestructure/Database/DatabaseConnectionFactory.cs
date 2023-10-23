using System.Data;
using Microsoft.Extensions.Configuration;
using MultiTenant.Domain.Interfaces;
using Npgsql;

namespace Multitenant.Infraestructure.Database;

public class DatabaseConnectionFactory :IDatabaseConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DatabaseConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IDbConnection CreateConnection(string dbConnection, string? tenantName)
    {
        var connectionString = string.Empty;
        switch(dbConnection)
        {
          case "UsersAndOrganizations":
              //var organizationConnString
                  connectionString= _configuration.GetConnectionString("UsersAndOrganizations");
              /*if (string.IsNullOrEmpty(tenantName))
              { 
                  connectionString = string.Format(organizationConnString, "master");
              }
              connectionString = string.Format(organizationConnString, tenantName);*/
              break;
           case "Products":
               var productConnString = _configuration.GetConnectionString("Products");
               if (string.IsNullOrEmpty(tenantName))
               { 
                   connectionString = string.Format(productConnString, "master");
               }
               connectionString = string.Format(productConnString, tenantName);
               break;
        }
        
        return new NpgsqlConnection(connectionString);
    }
}