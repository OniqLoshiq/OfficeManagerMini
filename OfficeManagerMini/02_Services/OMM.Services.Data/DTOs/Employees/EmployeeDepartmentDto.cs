using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Linq;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeDepartmentDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string ProfilePicture { get; set; }

        public int AccessLevel { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Position { get; set; }

        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }

        public int InProgressAssistantAssignments { get; set; }

        public int InProgressExecuterAssignments { get; set; }
        
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeDepartmentDto>()
                .ForMember(destination => destination.PhoneNumber,
                opts => opts.MapFrom(origin => origin.PhoneNumber ?? origin.PersonalPhoneNumber))
                .ForMember(destination => destination.InProgressAssistantAssignments,
                opts => opts.MapFrom(origin => origin.AssistantToAssignments.Where(a => a.Assignment.Status.Name == Constants.STATUS_INPROGRESS).Count()))
                .ForMember(destination => destination.InProgressExecuterAssignments,
                opts => opts.MapFrom(origin => origin.ExecutionAssignments.Where(a => a.Status.Name == Constants.STATUS_INPROGRESS).Count()));
        }
    }
}
