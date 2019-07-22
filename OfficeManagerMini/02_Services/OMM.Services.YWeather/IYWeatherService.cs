using OMM.Services.YWeather.YWeather;
using System.Threading.Tasks;

namespace OMM.Services.YWeather
{
    public interface IYWeatherService
    {
        Task<YWeatherInfo> GetWeatherInfoAsync();
    }
}
