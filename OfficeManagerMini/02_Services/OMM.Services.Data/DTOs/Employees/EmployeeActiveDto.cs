using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeActiveDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }
        
        public string FullName { get; set; }

        public string ProfilePicture { get; set; }

        public int AccessLevel { get; set; }

        public string DepartmentName { get; set; }

        public string Position { get; set; }

        public string AppointedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeActiveDto>()
                .ForMember(destination => destination.AppointedOn,
                opts => opts.MapFrom(origin => origin.AppointedOn.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
