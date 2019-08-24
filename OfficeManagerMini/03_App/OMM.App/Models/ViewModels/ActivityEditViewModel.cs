using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Activities;

namespace OMM.App.Models.ViewModels
{
    public class ActivityEditViewModel : IMapTo<ActivityEditDto>, IMapFrom<ActivityEditDto>
    {
        public string Id { get; set; }

        public string Date { get; set; }

        public string WorkingTime { get; set; }

        public string Description { get; set; }

        public string ReportId { get; set; }
    }
}
