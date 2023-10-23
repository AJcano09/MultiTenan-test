using System.Data;
using Dapper;
using MultiTenant.Domain.Interfaces;

namespace MultiTenant.Application.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IDbConnection _connection;
    private readonly string _tableName;

    protected GenericRepository(IDbConnection connection)
    {
        _connection = connection;
        _tableName = typeof(T).Name;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = $@"SELECT * FROM ""{_tableName}""";
        return await _connection.QueryAsync<T>(query);
    }

    public async Task<T> GetAsync(Guid id)
    {
        var query = $"SELECT * FROM \"{_tableName}\" WHERE \"Id\" = @Id";
        return await _connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
    }

    public async Task<int> InsertAsync(T entity)
    {
        var columns = String.Join(",", GetColumns(entity).Select(c => $"\"{c}\""));
        var parameters = String.Join(",", GetParameters(entity));
        var insertQuery = $"INSERT INTO \"{_tableName}\" ({columns}) VALUES ({parameters})";

        return await _connection.ExecuteAsync(insertQuery, entity);
    }

    public async Task<int> UpdateAsync(T entity)
    {
        // TODO : usar comillas dobles para postgres reconozcas la nomenclatura pascalCase
        var updateQuery = $"UPDATE {_tableName} SET {String.Join(",", GetColumnsWithParameters(entity))} WHERE Id = @Id";
        return await _connection.ExecuteAsync(updateQuery, entity);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        // TODO : usar comillas dobles para postgres reconozcas la nomenclatura pascalCase
        var deleteQuery = $"DELETE FROM {_tableName} WHERE Id = @Id";
        return await _connection.ExecuteAsync(deleteQuery, new { Id = id });
    }
    
    private IEnumerable<string> GetColumns(T entity)
    {
        return entity.GetType().GetProperties().Select(x => x.Name);
    }

    private IEnumerable<string> GetParameters(T entity)
    {
        return entity.GetType().GetProperties().Select(x => $"@{x.Name}");
    }

    private IEnumerable<string> GetColumnsWithParameters(T entity)
    {
        return entity.GetType().GetProperties().Select(x => $"{x.Name} = @{x.Name}");
    }
}