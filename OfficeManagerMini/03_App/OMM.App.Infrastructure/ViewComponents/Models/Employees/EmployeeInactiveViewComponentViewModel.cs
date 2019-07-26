using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class EmployeeInactiveViewComponentViewModel : IMapFrom<EmployeeInactiveDto>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string ProfilePicture { get; set; }

        public string DepartmentName { get; set; }

        public string Position { get; set; }

        public string LeavingReasonReason { get; set; }

        public string LeftOn { get; set; }
    }
}
