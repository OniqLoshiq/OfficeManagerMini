using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

namespace OMM.Services.Data.DTOs.Activities
{
    public class ActivityListDto : IMapFrom<Activity>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string Hours { get; set; }

        public string Minutes { get; set; }
        
        public string EmployeeFullName { get; set; }

        public string EmployeeId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Activity, ActivityListDto>()
                 .ForMember(destination => destination.Date,
                 opts => opts.MapFrom(origin => origin.Date.ToString(Constants.DATETIME_FORMAT)))
                 .ForMember(destination => destination.Hours,
                 opts => opts.MapFrom(origin => (origin.WorkingMinutes / Constants.MINUTES_IN_HOUR).ToString("D2")))
                 .ForMember(destination => destination.Minutes,
                 opts => opts.MapFrom(origin => (origin.WorkingMinutes % Constants.MINUTES_IN_HOUR).ToString("D2")));
        }
    }
}
