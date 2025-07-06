

namespace HRIS.models
{
    public class Leave
    {
        public int LeaveID { get; set; }
        public string? Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }

        public int EmployeeID { get; set; }
        public required Employee Employee { get; set; }
    }
}