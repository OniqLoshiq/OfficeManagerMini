using OMM.Services.Data.DTOs.Departments;
using System.Linq;

namespace OMM.Services.Data
{
    public interface IDepartmentsService
    {
        IQueryable<DepartmentNameDto> GetAllDepartmentNames();

        int GetDepartmentIdByName(string name);
    }
}
