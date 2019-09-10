using AutoMapper;
using OMM.Services.AutoMapper;
using OMM.Services.YWeather.YWeather;
using System;
using System.Collections.Generic;

namespace OMM.App.Infrastructure.ViewComponents.Models.YWeather
{
    public class WeatherForecastViewComponentViewModel : IMapFrom<YWeatherInfo>, IHaveCustomMappings
    {
        public string LocationCity { get; set; }

        public string LocationCountry { get; set; }
        
        public string CurrentMonth { get; set; }

        public string CurrentDayOfMonth { get; set; }

        public string CurrentDayOfWeek { get; set; }
        
        public string CurrentCode { get; set; }

        public int CurrentTemperature { get; set; }

        public string CurrentText { get; set; }

        public int WindSpeed { get; set; }

        public int AtmosphereHumidity { get; set; }

        public double AtmospherePressure { get; set; }

        public string AstronomySunrise { get; set; }

        public string AstronomySunset { get; set; }

        public List<ForecastViewComponentViewModel> WeekForecasts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<YWeatherInfo, WeatherForecastViewComponentViewModel>()
               .ForMember(destination => destination.CurrentDayOfMonth,
                           opts => opts.MapFrom(origin => DateTimeOffset.FromUnixTimeSeconds(origin.Current_Observation.PubDate).ToOffset(new TimeSpan(12, 0, 0)).Day))
               .ForMember(destination => destination.CurrentDayOfWeek,
                           opts => opts.MapFrom(origin => DateTimeOffset.FromUnixTimeSeconds(origin.Current_Observation.PubDate).ToOffset(new TimeSpan(12, 0, 0)).DayOfWeek))
               .ForMember(destination => destination.CurrentMonth,
                           opts => opts.MapFrom(origin => DateTimeOffset.FromUnixTimeSeconds(origin.Current_Observation.PubDate).ToOffset(new TimeSpan(12, 0, 0)).ToString("MMM")))
               .ForMember(destination => destination.WeekForecasts,
                           opts => opts.MapFrom(origin => origin.Forecasts))
               .ForMember(destination => destination.WindSpeed,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Wind.Speed))
               .ForMember(destination => destination.CurrentCode,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Condition.Code))
               .ForMember(destination => destination.CurrentTemperature,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Condition.Temperature))
               .ForMember(destination => destination.CurrentText,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Condition.Text))
               .ForMember(destination => destination.AtmosphereHumidity,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Atmosphere.Humidity))
               .ForMember(destination => destination.AtmospherePressure,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Atmosphere.Pressure))
               .ForMember(destination => destination.AstronomySunrise,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Astronomy.Sunrise))
               .ForMember(destination => destination.AstronomySunset,
                           opts => opts.MapFrom(origin => origin.Current_Observation.Astronomy.Sunset));
        }
    }
}
