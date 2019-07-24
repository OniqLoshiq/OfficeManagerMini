using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Departments;

namespace OMM.App.Infrastructure.ViewComponents.Models.Departments
{
    public class DepartmentListViewModel : IMapFrom<DepartmentListDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
