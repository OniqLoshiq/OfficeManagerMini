using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Reports;
using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class ReportDetailsViewModel : IMapFrom<ReportDetailsDto>
    {
        public string Id { get; set; }

        public ProjectReportViewModel Project { get; set; }

        public List<ActivityListViewModel> Activities { get; set; } = new List<ActivityListViewModel>();
    }
}
