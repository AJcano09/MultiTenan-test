using Dapper;
using Microsoft.AspNetCore.Http;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Database.Organization;

namespace MultiTenant.Application.Repositories;

public class UserRepository :  GenericRepository<User>
{
    private readonly IDbContext _organizationConnection;

    public UserRepository(OrganizationsDbContext organizationConnection,IHttpContextAccessor contextAccessor) 
        : base(organizationConnection.GetConnection(string.Empty))
    {
        _organizationConnection = organizationConnection;
    }
    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        using var dbConnection = _organizationConnection.GetConnection(string.Empty);
        dbConnection.Open();

        const string query = "SELECT * FROM public.\"User\" u WHERE u.\"Email\"  =@Email AND u.\"Password\" = @Password";
        var result = dbConnection.QuerySingleOrDefault<User>(query, new { email, password });
        return result;
    }
}