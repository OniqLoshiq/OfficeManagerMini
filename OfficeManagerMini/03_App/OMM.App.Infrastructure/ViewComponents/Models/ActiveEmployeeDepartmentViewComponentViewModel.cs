using AutoMapper;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Infrastructure.ViewComponents.Models
{
    public class ActiveEmployeeDepartmentViewComponentViewModel : IMapFrom<ActiveEmployeeDepartmentDto>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
