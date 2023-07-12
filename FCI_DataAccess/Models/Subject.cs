namespace FCI_DataAccess.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime AddedOn { get; set; } = DateTime.Now;
        //public int DepartmentId { get; set; }
        public List<Department> Departments { get; } = new();
        public List<SubjectDepartment> SubjectDepartments { get; set; } = new List<SubjectDepartment>();


    }
}
