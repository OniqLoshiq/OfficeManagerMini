using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.App.Models.ViewModels
{
    public class ProjectDetailsParticipantViewModel : IMapFrom <ProjectDetailsParticipantDto>
    {
        public string ProjectId { get; set; }

        public string ParticipantId { get; set; }

        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public int ProjectPositionId { get; set; }

        public string ProjectPositionName { get; set; }
    }
}
