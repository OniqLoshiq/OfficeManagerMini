using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Activities
{
    public class ActivityCreateDto : IMapTo<Activity>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string Date { get; set; }

        public string WorkingTime { get; set; }

        public string EmployeeId { get; set; }

        public string ReportId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ActivityCreateDto, Activity>()
                 .ForMember(destination => destination.WorkingMinutes,
                 opts => opts.MapFrom(origin =>
                 (int.Parse(origin.WorkingTime.Split( ':', StringSplitOptions.RemoveEmptyEntries)[0]) * 60) 
                 + int.Parse(origin.WorkingTime.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])))
                 .ForMember(destination => destination.Date,
                 opts => opts.MapFrom(origin => DateTime.ParseExact(origin.Date, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture)));
        }
}
}
