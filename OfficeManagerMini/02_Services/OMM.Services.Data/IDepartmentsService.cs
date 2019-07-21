using OMM.Services.Data.DTOs.Departments;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IDepartmentsService
    {
        IQueryable<DepartmentNameDto> GetAllDepartmentNames();

        Task<int> GetDepartmentIdByName(string name);
    }
}
