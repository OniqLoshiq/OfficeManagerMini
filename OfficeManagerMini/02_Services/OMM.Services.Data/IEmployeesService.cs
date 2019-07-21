using OMM.Services.Data.DTOs.Employees;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IEmployeesService
    {
        Task<bool> LoginEmployeeAsync(EmployeeLoginDto employeeDto);

        Task LogoutEmployee();

        Task<bool> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto);
    }
}
