using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeSideBarDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string ProfilePicture { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeSideBarDto>()
                .ForMember(destination => destination.EmployeeName,
                opts => opts.MapFrom(origin => origin.FirstName + " " + origin.LastName));
                
        }
    }
}
