using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IDepartmentsService
    {
        IQueryable<T> GetAllDepartmentsByDto<T>();

        Task<string> GetDepartmentNameByIdAsync(int departmentId);
    }
}
