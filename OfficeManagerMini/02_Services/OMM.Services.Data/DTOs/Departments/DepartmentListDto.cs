using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Departments
{
    public class DepartmentListDto : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
