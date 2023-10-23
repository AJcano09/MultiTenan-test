using Dapper;
using FluentMigrator.Expressions;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Enums;

namespace MultiTenant.Application.Repositories;

public class UserRepository :  GenericRepository<User>
{
    private readonly IDatabaseConnectionFactory _factory;

    public UserRepository(IDatabaseConnectionFactory factory) 
        : base(factory.CreateConnection(DbProvider.UsersAndOrganizations.ToString()))
    {
        _factory = factory;
    }
    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        using var dbConnection = _factory.CreateConnection(DbProvider.UsersAndOrganizations.ToString());
        dbConnection.Open();

        const string query = "SELECT * FROM public.\"User\" u WHERE u.\"Email\"  =@Email AND u.\"Password\" = @Password";
        var result = dbConnection.QuerySingleOrDefault<User>(query, new { email, password });
        dbConnection.Close();
        return result;
    }
}