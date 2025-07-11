namespace HRIS.models
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public required string Name { get; set; }
        public DateTime DOB { get; set; }
        public required string Gender { get; set; }
        public required string Contact { get; set; }
        public int DepID { get; set; }
        public required Department Department { get; set; }
    }
}