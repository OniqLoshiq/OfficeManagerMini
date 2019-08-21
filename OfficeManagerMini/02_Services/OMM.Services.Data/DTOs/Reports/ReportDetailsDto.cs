using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Activities;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Reports
{
    public class ReportDetailsDto : IMapFrom<Report>
    {
        public string Id { get; set; }

        public ProjectReportDto Project { get; set; }

        public List<ActivityListDto> Activities { get; set; } = new List<ActivityListDto>();
    }
}
