using FCI_API.Data;
using FCI_DataAccess.Models;
using FCI_DataAccess.Repository.IRepository;

namespace FCI_DataAccess.Repository
{
    /// <summary>
    /// Repository implementation for the Department entity.
    /// </summary>
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates a Department asynchronously.
        /// </summary>
        /// <param name="Department">The updated Department object.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated Department entity.</returns>
        public async Task<Department> UpdateDepartmentAsync(Department Department)
        {
            _context.Departments.Update(Department);
            await _context.SaveChangesAsync();
            return Department;
        }
    }
}