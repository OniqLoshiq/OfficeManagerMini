using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Activities;

namespace OMM.App.Models.ViewModels
{
    public class ActivityListViewModel : IMapFrom<ActivityListDto>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string Hours { get; set; }

        public string Minutes { get; set; }

        public string EmployeeFullName { get; set; }

        public string EmployeeId { get; set; }
    }
}
