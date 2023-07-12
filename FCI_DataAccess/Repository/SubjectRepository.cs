using FCI_API.Data;
using FCI_DataAccess.Models;
using FCI_DataAccess.Repository.IRepository;

namespace FCI_DataAccess.Repository
{

    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

    }
}