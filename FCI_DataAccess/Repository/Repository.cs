using FCI_API.Data;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FCI_DataAccess.Repository
{
    /// <summary>
    /// Generic repository class for performing CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> dbSet;

        /// <summary>
        /// Constructs a new instance of the Repository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        protected Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Asynchronously creates a new entity of type T.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The created entity.</returns>
        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        /// <summary>
        /// Asynchronously deletes an entity of type T.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>A boolean indicating the success or failure of the delete operation.</returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            var resalut = await SaveAsync();
            if (resalut > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type T.
        /// </summary>
        /// <param name="filter">Optional filter expression to apply filtering criteria.</param>
        /// <returns>A collection of entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);
            return query.ToList();
        }

        /// <summary>
        /// Asynchronously retrieves a single entity of type T.
        /// </summary>
        /// <param name="filter">Optional filter expression to specify the criteria for selecting the entity.</param>
        /// <param name="tracked">Indicates whether the entity should be tracked by the repository.</param>
        /// <returns>The selected entity.</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter is not null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Asynchronously saves any pending changes in the repository.
        /// </summary>
        /// <returns>The number of affected entities.</returns>
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<T>> GetAllIncludeProperties(string? includeProperties = null)
        {
            IQueryable<T> quary = dbSet;
            if (includeProperties != null)
            {
                foreach (var includepror in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary = quary.Include(includepror);
                }
            }
            return quary.ToList();
        }

    }
}
