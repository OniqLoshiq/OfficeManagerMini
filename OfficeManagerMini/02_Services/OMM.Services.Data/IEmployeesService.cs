using OMM.Services.Data.DTOs.Employees;
using System.Linq;
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

        IQueryable<EmployeeEditDto> GetEmployeeEditByIdAsync(string id);

        Task<bool> EditAsync(EmployeeEditDto employeeToEdit);
    }
}
