using System.Linq.Expressions;

namespace FCI_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Generic repository interface for performing CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously creates a new entity of type T.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The created entity.</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an entity of type T.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>A boolean indicating the success or failure of the delete operation.</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Asynchronously retrieves all entities of type T.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply filtering criteria.</param>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        /// <summary>
        /// Asynchronously retrieves a single entity of type T.
        /// </summary>
        /// <param name="filter">Optional filter expression to specify the criteria for selecting the entity.</param>
        /// <param name="tracked">Indicates whether the entity should be tracked by the repository.</param>
        /// <returns>The selected entity.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);

        /// <summary>
        /// Asynchronously saves any pending changes in the repository.
        /// </summary>
        /// <returns>The number of affected entities.</returns>
        Task<int> SaveAsync();




        public Task<IEnumerable<T>> GetAllIncludeProperties(string? includeProperties = null);
    }
}