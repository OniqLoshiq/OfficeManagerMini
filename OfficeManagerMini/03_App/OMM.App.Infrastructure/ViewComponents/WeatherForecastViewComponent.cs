using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Infrastructure.ViewComponents.Models.YWeather;
using OMM.Services.YWeather;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class WeatherForecastViewComponent : ViewComponent
    {
        private readonly IYWeatherService yWeatherService;

        public WeatherForecastViewComponent(IYWeatherService yWeatherService)
        {
            this.yWeatherService = yWeatherService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var weatherForecast = await this.yWeatherService.GetWeatherInfoAsync();

            var weatherForecastViewModel = Mapper.Map<WeatherForecastViewComponentViewModel>(weatherForecast);

            return View(weatherForecastViewModel);
        }
    }
}
