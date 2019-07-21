using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Departments
{
    public class DepartmentNameDto : IMapFrom<Department>
    {
        public string Name { get; set; }
    }
}
