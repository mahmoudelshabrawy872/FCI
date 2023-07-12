using FCI_DataAccess.Models;

namespace FCI_DataAccess.Repository.IRepository
{

    public interface ISubjectRepository : IRepository<Subject>
    {

        public Task<Subject> UpdateSubjectAsync(Subject subject);
    }
}