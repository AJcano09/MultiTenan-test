using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MultiTenant.Domain.Interfaces;
using Npgsql;

namespace Multitenant.Infraestructure.Database;

public class DatabaseConnectionFactory :IDatabaseConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _contextAccessor;

    public DatabaseConnectionFactory(IConfiguration configuration,IHttpContextAccessor contextAccessor)
    {
        _configuration = configuration;
        _contextAccessor = contextAccessor;
    }
    
    public IDbConnection CreateConnection(string dbConnection)
    {
        var tenantName = _contextAccessor.HttpContext.Items[Constants.SLUG_TENANT]?.ToString();
        var connectionString = string.Empty;
        switch(dbConnection)
        {
          case "UsersAndOrganizations":
                  connectionString= _configuration.GetConnectionString("UsersAndOrganizations");
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