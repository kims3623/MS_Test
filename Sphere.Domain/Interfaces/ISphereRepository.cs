using System.Linq.Expressions;
using Sphere.Domain.Common;

namespace Sphere.Domain.Interfaces;

/// <summary>
/// Generic repository interface for SPHERE entities with composite key support.
/// </summary>
/// <typeparam name="T">Entity type inheriting from SphereEntity</typeparam>
public interface ISphereRepository<T> where T : SphereEntity
{
    /// <summary>
    /// Gets an entity by its composite key values.
    /// </summary>
    /// <param name="keyValues">Composite key values in order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<T?> GetByKeyAsync(object[] keyValues, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Read-only list of all entities</returns>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Read-only list of matching entities</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entities by division sequence.
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Read-only list of entities in the division</returns>
    Task<IReadOnlyList<T>> GetByDivSeqAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets active entities (UseYn = "Y") by division sequence.
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Read-only list of active entities</returns>
    Task<IReadOnlyList<T>> GetActiveAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added entity</returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple entities.
    /// </summary>
    /// <param name="entities">Entities to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity (hard delete).
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft deletes an entity by setting UseYn = "N".
    /// </summary>
    /// <param name="entity">Entity to soft delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SoftDeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts entities matching the optional predicate.
    /// </summary>
    /// <param name="predicate">Optional filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Count of matching entities</returns>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any entity matches the predicate.
    /// </summary>
    /// <param name="predicate">Filter expression</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if any match exists</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a queryable for advanced queries.
    /// </summary>
    /// <returns>IQueryable for the entity</returns>
    IQueryable<T> Query();
}
