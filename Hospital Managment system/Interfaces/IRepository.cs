namespace Hospital_Managment_system.Interfaces;

/// <summary>
/// Generic repository interface for CRUD operations.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get all entities.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get entity by ID.
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Add a new entity.
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Update an entity.
    /// </summary>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Delete an entity.
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// Get paginated results.
    /// </summary>
    Task<(IEnumerable<T> items, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);

    /// <summary>
    /// Check if entity exists.
    /// </summary>
    Task<bool> ExistsAsync(int id);
}
