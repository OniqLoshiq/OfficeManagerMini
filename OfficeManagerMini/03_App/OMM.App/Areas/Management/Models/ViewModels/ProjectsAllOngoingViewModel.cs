using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class ProjectsAllOngoingViewModel : IMapFrom<ProjectAllListDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public string StatusName { get; set; }

        public int ParticipantsCount { get; set; }

        public int AssignmentsCount { get; set; }
    }
}
