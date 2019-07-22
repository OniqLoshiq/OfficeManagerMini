namespace OMM.Services.YWeather.YWeather
{
    public class YWeatherInfo
    {
        public Location Location { get; set; }
        public Current_Observation Current_Observation { get; set; }
        public Forecast[] Forecasts { get; set; }
    }
}
