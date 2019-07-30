using OMM.Services.Data.DTOs.Departments;
using System.Linq;

namespace OMM.Services.Data
{
    public interface IDepartmentsService
    {
        IQueryable<T> GetAllDepartmentsByDto<T>();

        string GetDepartmentNameById(int departmentId);
    }
}
