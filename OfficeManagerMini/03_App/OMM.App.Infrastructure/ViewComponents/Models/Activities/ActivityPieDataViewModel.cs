using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Activities;

namespace OMM.App.Infrastructure.ViewComponents.Models.Activities
{
    public class ActivityPieDataViewModel : IMapFrom<ActivityPieDataDto>
    {
        public int WorkingMinutes { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeFullName { get; set; }
    }
}
