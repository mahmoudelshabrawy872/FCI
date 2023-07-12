namespace FCI_DataAccess.Models
{
    public class SubjectDepartment
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = new Subject();
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = new Department();
    }
}
