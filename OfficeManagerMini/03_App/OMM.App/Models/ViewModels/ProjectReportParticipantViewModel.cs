using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.App.Models.ViewModels
{
    public class ProjectReportParticipantViewModel : IMapFrom<ProjectReportParticipantDto>
    {
        public string ParticipantId { get; set; }

        public string ProfilePicture { get; set; }

        public string ParticipantFullName { get; set; }

        public string DepartmentName { get; set; }

        public string ProjectPositionName { get; set; }
    }
}
