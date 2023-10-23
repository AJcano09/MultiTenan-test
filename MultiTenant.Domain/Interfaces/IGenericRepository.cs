namespace MultiTenant.Domain.Interfaces;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Guid id);
    Task<int> InsertAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(Guid id);
}