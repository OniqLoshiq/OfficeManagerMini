using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Employees
{
    public class ActiveEmployeeDepartmentDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Employee, ActiveEmployeeDepartmentDto>()
                .ForAllMembers(m => m.Condition(e => e.IsActive == true));
        }
    }
}
