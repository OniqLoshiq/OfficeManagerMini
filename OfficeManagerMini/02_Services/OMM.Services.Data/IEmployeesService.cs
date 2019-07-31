using OMM.Services.Data.DTOs.Employees;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IEmployeesService
    {
        Task<bool> LoginEmployeeAsync(EmployeeLoginDto employeeDto);

        Task LogoutEmployee();

        Task<bool> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto);

        IQueryable<ActiveEmployeeDepartmentDto> GetActiveEmployeesWithDepartment();

        IQueryable<EmployeeActiveDto> GetAllActiveEmployees();

        IQueryable<EmployeeInactiveDto> GetAllInactiveEmployees();

        IQueryable<T> GetEmployeeDtoByIdAsync<T>(string id);

        IQueryable<T> GetEmployeeDtoByUsernameAsync<T>(string username);

        Task<bool> EditAsync(EmployeeEditDto employeeToEdit);

        Task<bool> ReleaseAsync(EmployeeReleaseDto employeeToRelease);

        Task<bool> HireBackAsync(EmployeeHireBackDto employeeToHireBack);

        Task<bool> ValidateCurrentPasswordAsync(string employeeIdl, string currentPassword);

        Task<bool> ChangePasswordAsync(string employeeId, EmployeeChangePasswordDto changePasswordDto);

        bool IsEmailValid(string email);

        Task<bool> RetrievePasswordAsync(string email);

        Task<string> GetEmployeeFullNameByIdAsync(string currentEmployeeId);
    }
}
