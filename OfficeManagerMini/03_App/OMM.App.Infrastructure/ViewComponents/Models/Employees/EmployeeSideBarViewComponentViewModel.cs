using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class EmployeeSideBarViewComponentViewModel : IMapFrom<EmployeeSideBarDto>
    {
        public string ProfilePicture { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }

        public int MyAssignments { get; set; }

        public int AssignmentsAsAssistant { get; set; }
        
        public int AssignmentsFromMe { get; set; }

        public int AssignmentsForMe { get; set; }

        public int AllAssignments { get; set; }
    }
}
