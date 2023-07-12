namespace FCI_API.Dto.Subject
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime AddedOn { get; set; } = DateTime.Now;
        // public int DepartmentId { get; set; }
        public List<FCI_DataAccess.Models.Department> Departments { get; } = new();


    }
}
