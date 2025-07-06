using HRIS.models;

namespace HRIS.models
{
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string? Status { get; set; }

        public int EmployeeID { get; set; }
        public required Employee Employee { get; set; }
    }
}