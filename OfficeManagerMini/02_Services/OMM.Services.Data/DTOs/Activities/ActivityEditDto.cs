using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Activities
{
    public class ActivityEditDto : IMapFrom<Activity>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Date { get; set; }

        public string WorkingTime { get; set; }

        public string Description { get; set; }

        public string ReportId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Activity, ActivityEditDto>()
                .ForMember(destination => destination.WorkingTime,
                opts => opts.MapFrom(origin => (origin.WorkingMinutes / Constants.MINUTES_IN_HOUR).ToString("D2") + ":" + (origin.WorkingMinutes % Constants.MINUTES_IN_HOUR).ToString("D2")))
                .ForMember(destination => destination.Date,
                opts => opts.MapFrom(origin => origin.Date.ToString(Constants.ACTIVITY_DATETIME_FORMAT,CultureInfo.InvariantCulture)));
        }
    }
}
