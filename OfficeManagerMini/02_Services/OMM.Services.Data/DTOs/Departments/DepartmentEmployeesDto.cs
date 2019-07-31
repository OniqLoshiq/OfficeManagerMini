using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.Collections.Generic;
using System.Linq;

namespace OMM.Services.Data.DTOs.Departments
{
    public class DepartmentEmployeesDto : IMapFrom<Department>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public List<EmployeeDepartmentDto> Employees { get; set; } = new List<EmployeeDepartmentDto>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Department, DepartmentEmployeesDto>()
                .ForMember(dest => dest.Employees,
                opts => opts.MapFrom(d => d.Employees.Where(e => e.IsActive == true)));
        }
    }
}
