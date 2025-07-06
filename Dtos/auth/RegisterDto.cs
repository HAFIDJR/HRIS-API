using System.ComponentModel.DataAnnotations;

public enum UserRole
{
    HRD,
    KARYAWAN
}

public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
    public UserRole? Role { get; set; } = null;
    public int? EmployeeID { get; set; }
}