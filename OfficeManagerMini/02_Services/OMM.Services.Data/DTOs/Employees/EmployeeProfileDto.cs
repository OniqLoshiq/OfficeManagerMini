using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Assets;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeProfileDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string PersonalPhoneNumber { get; set; }

        public int AccessLevel { get; set; }

        public string DepartmentName { get; set; }

        public string Position { get; set; }

        public string AppointedOn { get; set; }

        public string PhoneNumber { get; set; }

        public List<AssetEmployeeDto> Items { get; set; } = new List<AssetEmployeeDto>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<Employee, EmployeeProfileDto>()
               .ForMember(destination => destination.DateOfBirth,
                           opts => opts.MapFrom(origin => origin.DateOfBirth.ToString(Constants.DATETIME_FORMAT)))
               .ForMember(destination => destination.AppointedOn,
                           opts => opts.MapFrom(origin => origin.AppointedOn.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
