using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class ProjectReportViewModel : IMapFrom<ProjectReportDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string Deadline { get; set; }

        public string EndDate { get; set; }

        public double Progress { get; set; }

        public string StatusName { get; set; }

        public List<ProjectReportParticipantViewModel> Participants { get; set; } = new List<ProjectReportParticipantViewModel>();
    }
}
