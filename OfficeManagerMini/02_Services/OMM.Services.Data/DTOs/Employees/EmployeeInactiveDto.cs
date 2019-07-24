using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeInactiveDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string ProfilePicture { get; set; }

        public string DepartmentName { get; set; }

        public string Position { get; set; }

        public string LeavingReasonName { get; set; }

        public string LeftOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeInactiveDto>()
                .ForMember(dest => dest.LeftOn,
                opts => opts.MapFrom(origin => origin.LeftOn.Value.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
