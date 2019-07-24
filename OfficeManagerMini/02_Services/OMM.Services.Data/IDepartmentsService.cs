using OMM.Services.Data.DTOs.Departments;
using System.Linq;

namespace OMM.Services.Data
{
    public interface IDepartmentsService
    {
        IQueryable<DepartmentListDto> GetAllDepartmentsList();

        string GetDepartmentNameById(int departmentId);
    }
}
