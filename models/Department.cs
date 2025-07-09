using System.ComponentModel.DataAnnotations;

namespace HRIS.models
{
    public class Department
    {
        [Key]
        public int DepID { get; set; }
        public required string Name { get; set; }
        public string? Location { get; set; }
        public int? ManagerID { get; set; }

        public Manager? Manager { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}