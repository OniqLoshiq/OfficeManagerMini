using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
namespace OMM.App.Models.ViewModels
{
    public class EmployeeDepartmentViewModel : IMapFrom<EmployeeDepartmentDto>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string ProfilePicture { get; set; }

        public int AccessLevel { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Position { get; set; }

        public string DepartmentName { get; set; }

        public int InProgressAssistantAssignments { get; set; }

        public int InProgressExecuterAssignments { get; set; }
    }
}
