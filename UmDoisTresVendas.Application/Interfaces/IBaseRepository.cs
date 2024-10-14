using System.Linq.Expressions;
using UmDoisTresVendas.Domain.Entities;

namespace UmDoisTresVendas.Application.Interfaces;

/// <summary>
/// Represents a generic repository interface for data access operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <returns>A collection of entities of type <typeparamref name="TEntity"/>.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity of type <typeparamref name="TEntity"/> if found; otherwise, <c>null</c>.</returns>
    Task<TEntity> GetByIdAsync(Guid id);

    /// <summary>
    /// Adds a new entity of type <typeparamref name="TEntity"/> to the repository.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>The added entity of type <typeparamref name="TEntity"/>.</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="TEntity"/> in the repository.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity of type <typeparamref name="TEntity"/>.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to be deleted.</param>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Finds entities that match the specified criteria.
    /// </summary>
    /// <param name="predicate">An expression that defines the conditions of the entities to be retrieved.</param>
    /// <returns>A collection of entities of type <typeparamref name="TEntity"/> that match the specified criteria.</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
}
