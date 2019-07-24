using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Employees
{
    public class ActiveEmployeeDepartmentDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Employee, ActiveEmployeeDepartmentDto>()
                .ForMember(destination => destination.DepartmentName,
                options => options.MapFrom(origin => origin.Department.Name));
        }
    }
}
