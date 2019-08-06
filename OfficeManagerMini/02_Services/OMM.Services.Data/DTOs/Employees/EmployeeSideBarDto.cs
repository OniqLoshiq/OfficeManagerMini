using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Linq;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeSideBarDto : IMapFrom<Employee>, IHaveCustomMappings
    {
        public string ProfilePicture { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }

        public int MyAssignments => AssignmentsAsAssistant + AssignmentsFromMe + AssignmentsForMe;

        public int AssignmentsAsAssistant { get; set; }
        
        public int AssignmentsFromMe { get; set; }

        public int AssignmentsForMe { get; set; }

        public int AllAssignments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeSideBarDto>()
                .ForMember(destination => destination.EmployeeName,
                opts => opts.MapFrom(origin => origin.FirstName + " " + origin.LastName))
                .ForMember(destination => destination.AssignmentsFromMe,
                opts => opts.MapFrom(origin => origin.AssignedAssignments.Where(a => a.Status.Name != Constants.STATUS_INPROGRESS).Count()))
                .ForMember(destination => destination.AssignmentsForMe,
                opts => opts.MapFrom(origin => origin.ExecutionAssignments.Where(a => a.Status.Name != Constants.STATUS_INPROGRESS).Count()))
                .ForMember(destination => destination.AssignmentsAsAssistant,
                opts => opts.MapFrom(origin => origin.AssistantToAssignments.Where(a => a.Assignment.Status.Name != Constants.STATUS_INPROGRESS).Count()));
                
        }
    }
}
