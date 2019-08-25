using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Models.ViewModels
{
    public class EmployeeIndexViewModel : IMapFrom<EmployeeIndexDto>
    {
        public int MyProjects { get; set; }

        public int MyAssignments { get; set; }

        public int AssignmentsAsAssistant { get; set; }

        public int AssignmentsFromMe { get; set; }

        public int AssignmentsForMe { get; set; }
    }
}
