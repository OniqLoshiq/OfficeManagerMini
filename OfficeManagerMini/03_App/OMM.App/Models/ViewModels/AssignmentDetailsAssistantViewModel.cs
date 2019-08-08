using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;

namespace OMM.App.Models.ViewModels
{
    public class AssignmentDetailsAssistantViewModel : IMapFrom<AssignmentDetailsAssistantDto>
    {
        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }
    }
}
