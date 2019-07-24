using AutoMapper;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class ActiveEmployeeDepartmentViewModel : IMapFrom<ActiveEmployeeDepartmentDto>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }
    }
}
