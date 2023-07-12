

namespace FCI_API.Dto.Department
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime AddedOn { get; set; } = DateTime.Now;
        // public int DepartmentId { get; set; }
        public List<FCI_DataAccess.Models.Subject> subjects { get; set; } = new();
    }
}
