using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentDetailsAssistantDto : IMapFrom<Employee>
    {
        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }
    }
}
