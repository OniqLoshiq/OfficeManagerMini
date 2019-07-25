using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeEditDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string PersonalPhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        public int AccessLevel { get; set; }

        public int DepartmentId { get; set; }

        public string Position { get; set; }

        public string AppointedOn { get; set; }

        public string PhoneNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Employee, EmployeeEditDto>()
                .ForMember(destination => destination.DateOfBirth,
                            opts => opts.MapFrom(origin => origin.DateOfBirth.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.AppointedOn,
                            opts => opts.MapFrom(origin => origin.AppointedOn.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
