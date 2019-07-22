using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Models;
using OMM.Services.YWeather;

namespace OMM.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IYWeatherService yWeatherService;

        public HomeController(IYWeatherService yWeatherService)
        {
            this.yWeatherService = yWeatherService;
        }

        public async Task<IActionResult> Index()
        {
            var info = await this.yWeatherService.GetWeatherInfoAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
