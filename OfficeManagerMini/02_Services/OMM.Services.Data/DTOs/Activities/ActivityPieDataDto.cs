using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Activities
{
    public class ActivityPieDataDto : IMapFrom<Activity>
    {
        public int WorkingMinutes { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeFullName { get; set; }
    }
}
