using FCI_DataAccess.Models;

namespace FCI_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Interface for the Department repository, extending the generic repository.
    /// </summary>
    public interface IDepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// Updates a Department asynchronously.
        /// </summary>
        /// <param name="department">The updated Department object.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated Department entity.</returns>
        public Task<Department> UpdateDepartmentAsync(Department department);
    }
}