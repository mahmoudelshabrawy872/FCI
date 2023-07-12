namespace FCI_DataAccess.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime AddedOn { get; set; } = DateTime.Now;
        // public int SubjectId { get; set; }
        public List<SubjectDepartment> SubjectDepartments { get; set; } = new List<SubjectDepartment>();

    }
}
