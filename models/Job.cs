namespace HRIS.models
{
    public class Job
    {
        public int JobID { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? SalaryRange { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}