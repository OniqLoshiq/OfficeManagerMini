namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeLoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
