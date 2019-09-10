using AutoMapper;
using OMM.Services.AutoMapper;
using OMM.Services.YWeather.YWeather;
using System;

namespace OMM.App.Infrastructure.ViewComponents.Models.YWeather
{
    public class ForecastViewComponentViewModel : IMapFrom<Forecast>, IHaveCustomMappings
    {
        public string DayOfWeek { get; set; }

        public int Low{ get; set; }

        public int High { get; set; }

        public string Code { get; set; }

        public string Text { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Forecast, ForecastViewComponentViewModel>()
                .ForMember(destination => destination.DayOfWeek,
                            opts => opts.MapFrom(origin => DateTimeOffset.FromUnixTimeSeconds(origin.Date).ToOffset(new TimeSpan(12,0,0)).DayOfWeek));
        }
    }
}
