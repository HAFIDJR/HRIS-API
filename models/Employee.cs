namespace HRIS.models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public required string Name { get; set; }
        public DateTime DOB { get; set; }
        public required string Gender { get; set; }
        public int JobID { get; set; }
        public Job? Job { get; set; }
        public int DepID { get; set; }
        public Department? Department { get; set; }
        public ApplicationUser? User { get; set; }
    }
}