using Microsoft.AspNetCore.Identity;

namespace HRIS.models
{
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeID { get; set; }
        public Employee? Employee { get; set; }
    }
}