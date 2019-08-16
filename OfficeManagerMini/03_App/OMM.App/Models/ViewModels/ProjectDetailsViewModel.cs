using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class ProjectDetailsViewModel : IMapFrom<ProjectDetailsDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string ReportId { get; set; }

        public ProjectDetailsChangeViewModel ChangeData { get; set; }

        public List<ProjectDetailsParticipantViewModel> Participants { get; set; } = new List<ProjectDetailsParticipantViewModel>();
    }
}
