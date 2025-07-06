namespace HRIS.models
{
    public class User
    {
        public int UserID { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? Role { get; set; }
        public int? EmployeeID { get; set; }
    }
}